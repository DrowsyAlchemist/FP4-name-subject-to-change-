using UnityEngine;

public sealed class EnemyOrderSpawner : SequenceSpawner
{
    [SerializeField] private EnemySpawner _enemySpawner;

    private EnemyOrder _enemyOrder;

    protected override void Awake()
    {
        base.Awake();
        _enemySpawner.EnemySpawned += OnEnemySpawned;
    }

    private void OnDestroy()
    {
        _enemySpawner.EnemySpawned -= OnEnemySpawned;
    }

    public void SpawnOrder(EnemyOrder order)
    {
        _enemyOrder = order ?? throw new System.ArgumentNullException();
        SpawnSequence(order);
    }

    protected override void SpawnNext()
    {
        _enemySpawner.Spawn(_enemyOrder.EnemyTemplate);
    }

    private void OnEnemySpawned(Enemy _)
    {
        enabled = true;
    }
}