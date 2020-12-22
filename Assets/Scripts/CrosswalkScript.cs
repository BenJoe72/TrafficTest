using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(BoxCollider))]
public class CrosswalkScript : MonoBehaviour
{
    [SerializeField] private MinMaxFloat _SpawnTimeRange;
    [SerializeField] private PedestrianScript _PedestrianPrefab;
    [SerializeField] private Transform _LeftSide;
    [SerializeField] private Transform _RightSide;

    public bool HasPedestrianCrossing { get { return _activePedestrians.Any(x => !x.HasCrossed); } }

    private List<PedestrianScript> _activePedestrians;

    private float _nextSpawnTime;

    private void Start()
    {
        _nextSpawnTime = Time.time + _SpawnTimeRange.GetRandomValue();
        _activePedestrians = new List<PedestrianScript>();
    }

    private void Update()
    {
        CheckPedestrianTimer();
    }

    /// <summary>
    /// Checks if it's time to spawn a new pedestrian
    /// </summary>
    private void CheckPedestrianTimer()
    {
        if (Time.time >= _nextSpawnTime)
            SpawnPedestrian();
    }

    /// <summary>
    /// Spawns a pedestrian on one side of the crosswalk
    /// </summary>
    private void SpawnPedestrian()
    {
        bool startLeft = RandomHelper.CoinFlip();
        Vector3 position = startLeft ? _LeftSide.position : _RightSide.position;
        Vector3 destination = startLeft ? _RightSide.position : _LeftSide.position;

        PedestrianScript newPed = Instantiate(_PedestrianPrefab, position, Quaternion.identity, transform);
        newPed.SetDestination(destination, () => { _activePedestrians.Remove(newPed); });
        newPed.transform.LookAt(destination);

        _activePedestrians.Add(newPed);

        _nextSpawnTime = Time.time + _SpawnTimeRange.GetRandomValue();
    }
}
