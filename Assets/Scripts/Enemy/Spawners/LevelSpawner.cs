using UnityEngine;

public sealed class LevelSpawner : SequenceSpawner
{
    [SerializeField] private EnemyWaveSpawner _enemyWaveSpawner;

    private bool _isStarted;
    private LevelSetup _levelSetup;

    protected override void Awake()
    {
        base.Awake();
        _enemyWaveSpawner.SequenceFinished += OnWaveFinished;
    }

    private void OnDestroy()
    {
        _enemyWaveSpawner.SequenceFinished -= OnWaveFinished;
    }

    public void StartLevel(LevelSetup levelSetup)
    {
        if (_isStarted)
            throw new System.InvalidOperationException("Level is already started.");

        _levelSetup = levelSetup ?? throw new System.ArgumentNullException();
        _isStarted = true;
        SpawnSequence(levelSetup);
    }

    protected override void SpawnNext()
    {
        _enemyWaveSpawner.SpawnWave(_levelSetup.GetWave(SpawnedSequencesCount));
    }

    private void OnWaveFinished()
    {
        enabled = true;
    }
}