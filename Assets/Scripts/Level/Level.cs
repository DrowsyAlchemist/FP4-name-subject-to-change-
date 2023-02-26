using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private List<LevelSetup> _levelSetups;

    public int CurrentLevel { get; private set; } = -1;

    public event Action EnemySpawnFinished;

    private void Awake()
    {
        _levelSpawner.SequenceFinished += OnLevelFinished;
    }

    public void AbortSpawn()
    {
        _levelSpawner.Abort();
    }

    public void StartLevel(int level)
    {
        if (level < 0)
            throw new System.ArgumentOutOfRangeException();

        CurrentLevel = level;
        _levelSpawner.StartSpawn(_levelSetups[CurrentLevel]);
    }

    //public void StartNextLevel()
    //{
    //    CurrentLevel++;

    //    if (CurrentLevel < _levelSetups.Count)
    //    {
    //        _levelSpawner.StartSpawn(_levelSetups[CurrentLevel]);
    //    }
    //    else
    //    {
    //        Debug.Log("Levels ran over.");
    //    }
    //}

    private void OnDestroy()
    {
        _levelSpawner.SequenceFinished -= OnLevelFinished;
    }

    private void OnLevelFinished()
    {
        EnemySpawnFinished?.Invoke();
    }
}
