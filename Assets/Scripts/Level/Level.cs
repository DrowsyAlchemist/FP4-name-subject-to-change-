using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelSetup _levelSetup;
    [SerializeField] private EnemyWaveSpawner _enemyWaveSpawner;

    private static Level _instance;
    private int _spawnedWavesCount;
    private float _elapsedTime;

    public bool IsFinished => _spawnedWavesCount == _levelSetup.WavesCount;

    public event Action LevelFinished;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        _enemyWaveSpawner.WaveFinished += OnWaveFinished;
        enabled = false;
    }

    private void Update()
    {
        if (_elapsedTime > _levelSetup.SecondsBetweenWaves)
            StartNextWave();

        _elapsedTime += Time.deltaTime;
    }

    private void OnDestroy()
    {
        _enemyWaveSpawner.WaveFinished -= OnWaveFinished;
    }

    public void StartNextWave()
    {
        if (_enemyWaveSpawner.IsWaveFinished == false)
            throw new InvalidOperationException("Previous wave is still running.");

        if (IsFinished)
            throw new InvalidOperationException("Level is finished.");

        _enemyWaveSpawner.SpawnWave(_levelSetup.GetWave(_spawnedWavesCount));
        _spawnedWavesCount++;
        _elapsedTime = 0;
    }

    private void OnWaveFinished()
    {
        if (IsFinished)
            LevelFinished?.Invoke();
        else
            enabled = true;
    }
}