using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int _loopLevels = 10;
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private List<LevelSetup> _levelSetups;

    public int CurrentLevel { get; private set; } = -1;

    public event Action EnemySpawnFinished;

    private void Awake()
    {
        _levelSpawner.SequenceFinished += OnLevelFinished;
    }

    private void OnDestroy()
    {
        _levelSpawner.SequenceFinished -= OnLevelFinished;
    }

    public void StartLevel(int level)
    {
        if (level < 0)
            throw new ArgumentOutOfRangeException();

        CurrentLevel = level;

        while (level >= _levelSetups.Count)
            level -= _loopLevels;

        _levelSpawner.StartSpawn(_levelSetups[level]);
    }

    public void AbortSpawn()
    {
        _levelSpawner.Abort();
    }

    private void OnLevelFinished()
    {
        EnemySpawnFinished?.Invoke();
    }
}