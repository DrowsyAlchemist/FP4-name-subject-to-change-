using UnityEngine;

public sealed class EnemyWaveSpawner : SequenceSpawner
{
    [SerializeField] private EnemyOrderSpawner _orderSpawner;

    private EnemyWave _enemyWave;

    protected override void Awake()
    {
        base.Awake();
        _orderSpawner.SequenceFinished += OnOrderSpawned;
    }

    private void OnDestroy()
    {
        _orderSpawner.SequenceFinished -= OnOrderSpawned;
    }

    public void SpawnWave(EnemyWave wave)
    {
        _enemyWave = wave ?? throw new System.ArgumentNullException();
        SpawnSequence(wave);
    }

    protected override void SpawnNext()
    {
        _orderSpawner.SpawnOrder(_enemyWave.GetSequence(SpawnedSequencesCount));
    }

    private void OnOrderSpawned()
    {
        enabled = true;
    }
}