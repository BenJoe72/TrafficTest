using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class CarIntersectionBehaviour : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector3> OnNewDestinationSelected;
    [SerializeField] private UnityEvent OnIntersectionWait;
    [SerializeField] private UnityEvent OnIntersectionGo;

    private IntersectionPointScript _nextIntersection;

    private bool _waitingAtIntersection = false;

    private void Update()
    {
        CheckIntersection();
    }

    /// <summary>
    /// Sets the starting intersection
    /// </summary>
    /// <param name="start">The starting intersection</param>
    public void SetStartIntersection(IntersectionPointScript start)
    {
        _nextIntersection = start;
    }

    /// <summary>
    /// Performs checks on intersection when entering it
    /// </summary>
    public void EnterIntersection()
    {
        if (!_nextIntersection.CheckFree())
        {
            OnIntersectionWait?.Invoke();
            _waitingAtIntersection = true;
        }
    }

    /// <summary>
    /// Gets a new destination and resets variables used for the car movement
    /// </summary>
    public void NextDestination()
    {
        // Set intersections
        _nextIntersection = _nextIntersection.GetNextIntersection();

        OnNewDestinationSelected?.Invoke(_nextIntersection.Position);
    }

    /// <summary>
    /// Checks if the current intersection has been cleared up or not
    /// </summary>
    private void CheckIntersection()
    {
        if (!_waitingAtIntersection)
            return;

        if (_nextIntersection.CheckFree())
        {
            OnIntersectionGo?.Invoke();
            _waitingAtIntersection = false;
        }
    }
}
