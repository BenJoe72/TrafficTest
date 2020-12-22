using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PedestrianDetectorScript : MonoBehaviour
{
    [SerializeField] private UnityEvent PedestrianDetected;
    [SerializeField] private UnityEvent PedestrianEmpty;

    private bool _crossing;
    private CrosswalkScript _occupiedCrosswalk;

    private void LateUpdate()
    {
        CheckCurrentCrossingEmpty();
    }

    /// <summary>
    /// Checks if the currently detected crosswalk ahead is still occupied or not
    /// </summary>
    private void CheckCurrentCrossingEmpty()
    {
        if (!_crossing)
            return;

        if (!_occupiedCrosswalk.HasPedestrianCrossing)
        {
            _crossing = false;
            PedestrianEmpty?.Invoke();
        }
    }

    /// <summary>
    /// Checks whether pedestrians were detected in the crosswalk ahead
    /// </summary>
    /// <param name="colliders">The detected crosswalk(s)</param>
    public void PedestriansDetected(Collider[] colliders)
    {
        _crossing = false;

        foreach (var collider in colliders)
        {
            CrosswalkScript crosswalk = collider.GetComponent<CrosswalkScript>();

            if (crosswalk != null)
            {
                if (crosswalk.HasPedestrianCrossing)
                {
                    _crossing = true;
                    _occupiedCrosswalk = crosswalk;
                    break;
                }
            }
        }

        if (_crossing)
            PedestrianDetected?.Invoke();
    }
}
