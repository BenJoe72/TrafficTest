using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

public class CarFollowBehaviour : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> _OnSetCarFollowSpeed;
    [SerializeField] private float _SpeedModifier;

    public void OnFollowDetected(Collider[] colliders)
    {
        CarMoverScript firstCar = colliders.FirstOrDefault(x => x.GetComponent<CarMoverScript>() != null)?.GetComponent<CarMoverScript>();

        if (firstCar != null)
            _OnSetCarFollowSpeed?.Invoke(firstCar.CarSpeed + _SpeedModifier);
    }
}
