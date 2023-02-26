using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Wall _wall;

    public event Action<Enemy> EnemySpawned;

    public void Spawn(Enemy template, bool hasShield = false)
    {
        if (template == null)
            throw new ArgumentNullException();

            StartCoroutine(SpawnEnemy(template, hasShield));
    }

    private IEnumerator SpawnEnemy(Enemy template, bool hasShield)
    {
        yield return new WaitForEndOfFrame();
        int spawnPointIndex = UnityEngine.Random.Range(0, _spawnPoints.Length);
        Transform spawnPoint = _spawnPoints[spawnPointIndex];
        Enemy enemy = Instantiate(template, spawnPoint.position, Quaternion.identity, null);
        enemy.Init(_wall, hasShield);
        EnemySpawned?.Invoke(enemy);
    }
}