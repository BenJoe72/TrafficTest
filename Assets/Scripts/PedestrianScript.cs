using UnityEngine;
using System.Collections;
using System;

public class PedestrianScript : MonoBehaviour
{
    [SerializeField] private MinMaxFloat SpeedRange;
    [SerializeField] private MinMaxFloat WaitTimeRange;

    public bool HasCrossed { get; private set; }
    
    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private float _startTime;
    private float _moveTime;
    private float _finishTime;
    private float _curretnMoveTime;

    private Action _destroyCallback;

    private void Update()
    {
        MovePedestrian();
        CheckArrived();
    }

    /// <summary>
    /// Set the destination for the pedestrian
    /// Usually this is the other side of the crosswalk
    /// </summary>
    /// <param name="destination">The pedestrian's destination</param>
    public void SetDestination(Vector3 destination, Action destroyCallback)
    {
        // Set positions
        _startPosition = transform.position;
        _endPosition = destination;

        // Set timers
        _startTime = Time.time + WaitTimeRange.GetRandomValue();
        _moveTime = Vector3.Distance(_startPosition, _endPosition) / SpeedRange.GetRandomValue();
        _finishTime = _startTime + _moveTime + WaitTimeRange.GetRandomValue();

        _destroyCallback = destroyCallback;

        HasCrossed = false;
    }
    
    /// <summary>
    /// Checks if the pedestrian arrived on the other side
    /// </summary>
    private void CheckArrived()
    {
        if (Time.time >= _startTime + _moveTime)
            HasCrossed = true;

        if (Time.time >= _finishTime)
            Arrived();
    }

    /// <summary>
    /// Moves the pedestrian forward
    /// </summary>
    private void MovePedestrian()
    {
        if (Time.time < _startTime)
            return;

        float movePercent = _curretnMoveTime / _moveTime;
        transform.position = Vector3.Lerp(_startPosition, _endPosition, movePercent);
        _curretnMoveTime += Time.deltaTime;
    }

    /// <summary>
    /// Removes the pedestrian once they arrived on the other side
    /// </summary>
    private void Arrived()
    {
        _destroyCallback?.Invoke();
        Destroy(gameObject);
    }
}
