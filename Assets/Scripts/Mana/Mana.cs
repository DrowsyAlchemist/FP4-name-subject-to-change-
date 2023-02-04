using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private ManaRenderer _manaRenderer;

    private EnemySpawner _enemySpawner;

    public ManaStorage ManaStorage { get; private set; }

    public void Init (EnemySpawner enemySpawner)
    {
        ManaStorage = new ManaStorage();
        _manaRenderer.Render(ManaStorage);
        _enemySpawner = enemySpawner;
        enemySpawner.EnemySpawned += OnEnemySpawned;
    }

    private void OnDestroy()
    {
        _enemySpawner.EnemySpawned -= OnEnemySpawned;
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        enemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;
        ManaStorage.TakeMana(enemy.Reward);
    }
}