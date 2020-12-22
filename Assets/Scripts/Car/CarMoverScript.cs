using UnityEngine;
using UnityEngine.Events;

public class CarMoverScript : MonoBehaviour
{
    [SerializeField] private CarDefinition _Data;
    [SerializeField] private UnityEvent _OnDestinationReached;

    public float CarSpeed { get; private set; }

    private Vector3 _startPosition;
    private Vector3 _destinationPosition;
    private Vector3 _travelDirection;
    private bool _movementEnabled = true;
    private bool _waitingAtIntersection = true;
    
    private void Start()
    {
        _OnDestinationReached?.Invoke();
        CarSpeed = _Data.Speed;
    }

    private void Update()
    {
        CheckReachedDestination();
        MoveCar();
    }

    /// <summary>
    /// Slows the car down to its slow speed
    /// </summary>
    public void OverrideSpeed(float newSpeed)
    {
        // Only override variable if it's slower to avoid collisions
        if (newSpeed < CarSpeed)
            CarSpeed = newSpeed;
    }

    /// <summary>
    /// Resets the car speed to the one set in the data variable
    /// </summary>
    public void ResetSpeed()
    {
        CarSpeed = _Data.Speed;
    }

    /// <summary>
    /// Disables car movement
    /// </summary>
    public void StopCar()
    {
        _movementEnabled = false;
    }

    /// <summary>
    /// Enables car movement
    /// </summary>
    public void StartCar()
    {
        _movementEnabled = true;
    }
    
    /// <summary>
    /// Sets the next position and updates the start and current positions accordingly
    /// </summary>
    /// <param name="destination">The new destionation position</param>
    public void SetNextPosition(Vector3 destination)
    {
        // Set position
        transform.position = _destinationPosition;
        _startPosition = _destinationPosition;
        _destinationPosition = destination;
        _travelDirection = (_destinationPosition - _startPosition).normalized;

        // Set facing
        transform.LookAt(_destinationPosition);
    }

    /// <summary>
    /// Sets the starting position of the car
    /// </summary>
    /// <param name="startPosition">The intersection at which the car will start</param>
    public void SetStartPosition(Vector3 startPosition)
    {
        _startPosition = startPosition;
        _destinationPosition = startPosition;
    }

    /// <summary>
    /// Checks if the next intersection has already been reached or not
    /// If it has been reached then it gets the next destination
    /// </summary>
    private void CheckReachedDestination()
    {
        if (_movementEnabled && Vector3.SqrMagnitude(_destinationPosition - transform.position) <= 0.0025f)
        {
            _OnDestinationReached?.Invoke();
        }
    }

    /// <summary>
    /// Moves the car between the previous and the next intersection
    /// </summary>
    private void MoveCar()
    {
        if (_movementEnabled)
            transform.position += _travelDirection * CarSpeed * Time.deltaTime;
    }
}
