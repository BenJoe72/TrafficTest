using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private IntersectionPointScript[] _StartPositions;
    [SerializeField] private int _StartingCars;
    [SerializeField] private MinMaxFloat _TimeBetweenSpawns;
    [SerializeField] private CarMoverScript[] _CarPrefabs;
    [SerializeField] private bool _AutoSpawn = true;

    private float _nextSpawn;

    private void Start()
    {
        for (int i = 0; i < _StartingCars; i++)
            Invoke("ForceSpawnCar", .3f * i);

        _nextSpawn = Time.time + _TimeBetweenSpawns.GetRandomValue();
    }

    private void Update()
    {
        SpawnNewCar();

        if (Input.GetKeyDown(KeyCode.Space))
            ForceSpawnCar();

        if (Input.GetKeyDown(KeyCode.R))
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void ForceSpawnCar()
    {
        SpawnNewCar(true);
    }

    /// <summary>
    /// Tries to spawn a new car if the next spawning time has come
    /// </summary>
    private void SpawnNewCar(bool ignoreTimer = false)
    {
        if ((Time.time < _nextSpawn && !ignoreTimer) || !_AutoSpawn)
            return;

        IntersectionPointScript startPoint = _StartPositions.GetRandomElement();
        CarMoverScript newCar = Instantiate(_CarPrefabs.GetRandomElement(), startPoint.Position, Quaternion.identity, transform);
        CarIntersectionBehaviour newCarIB = newCar.GetComponent<CarIntersectionBehaviour>();

        newCar.SetStartPosition(startPoint.Position);
        newCarIB?.SetStartIntersection(startPoint);

        _nextSpawn = Time.time + _TimeBetweenSpawns.GetRandomValue();
    }
}
