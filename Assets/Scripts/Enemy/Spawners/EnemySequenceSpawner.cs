using System;
using UnityEngine;

public class EnemySequenceSpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;

    private EnemySequence _currentSequence;
    private int _spawnedEnemyCount;
    private float _elapsedTime;
    private bool _isSequenceFinished = true;

    public event Action SequenceFinished;

    private void Awake()
    {
        enabled = false;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _currentSequence.SpawnDelay)
        {
            _elapsedTime = 0;
            SpawnEnemy();

            if (_spawnedEnemyCount == _currentSequence.EnemyCount)
                FinishSpawning();
        }
    }

    public void SpawnSequence(EnemySequence sequence)
    {
        if (_isSequenceFinished == false)
            throw new InvalidOperationException("Previous sequence is not finished.");

        _currentSequence = sequence ?? throw new ArgumentNullException();
        _isSequenceFinished = false;
        _spawnedEnemyCount = 0;
        _elapsedTime = 0;
        enabled = true;
    }

    private void SpawnEnemy()
    {
        _enemySpawner.Spawn(_currentSequence.EnemyTemplate);
        _spawnedEnemyCount++;
    }

    private void FinishSpawning()
    {
        SequenceFinished?.Invoke();
        _isSequenceFinished = true;
        enabled = false;
    }
}