using UnityEngine;

public sealed class LevelSpawner : SequenceSpawner
{
    [SerializeField] private EnemyWaveSpawner _enemyWaveSpawner;

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

    public void StartSpawn(LevelSetup levelSetup)
    {
        _levelSetup = levelSetup ?? throw new System.ArgumentNullException();
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