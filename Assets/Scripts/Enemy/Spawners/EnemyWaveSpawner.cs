using System;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] private EnemySequenceSpawner _sequenceSpawner;

    private Wave _currentWave;
    private int _spawnedSequencesCount;
    private float _elapsedTime;

    public bool IsWaveFinished { get; private set; } = true;

    public event Action WaveFinished;

    private void Awake()
    {
        enabled = false;
        _sequenceSpawner.SequenceFinished += OnSequenceFinished;
    }

    private void OnDestroy()
    {
        _sequenceSpawner.SequenceFinished -= OnSequenceFinished;
    }

    private void Update()
    {
        if (_spawnedSequencesCount == _currentWave.SequenceCount)
        {
            FinishSpawning();
            return;
        }

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _currentWave.SecondsBetweenSequences)
        {
            _elapsedTime = 0;
            SpawnSequence();
            enabled = false;
        }
    }

    public void SpawnWave(Wave wave)
    {
        if (IsWaveFinished == false)
            throw new InvalidOperationException("Previous sequence is not finished.");

        _currentWave = wave ?? throw new ArgumentNullException();
        _spawnedSequencesCount = 0;
        _elapsedTime = 0;
        enabled = true;
    }

    private void SpawnSequence()
    {
        _sequenceSpawner.SpawnSequence(_currentWave.GetSequence(_spawnedSequencesCount));
        _spawnedSequencesCount++;
    }

    private void FinishSpawning()
    {
        WaveFinished?.Invoke();
        IsWaveFinished = true;
        enabled = false;
    }

    private void OnSequenceFinished()
    {
        enabled = true;
    }
}