using System.Collections.Generic;

public class AliveEnemiesHolder
{
    private List<Enemy> _aliveEnemies = new();

    public int Count => _aliveEnemies.Count;

    public AliveEnemiesHolder(EnemySpawner enemySpawner)
    {
        enemySpawner.EnemySpawned += OnEnemySpawned;
    }

    public void StopAllEnemies()
    {
        foreach (var enemy in _aliveEnemies)
            enemy.StopAction();
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        enemy.Died += OnEnemyDied;
        _aliveEnemies.Add(enemy);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;
        _aliveEnemies.Remove(enemy);
    }
}