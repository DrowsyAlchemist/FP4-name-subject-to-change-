using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    public event Action<Enemy> EnemySpawned;

    public void Spawn(Enemy template)
    {
        if (template == null)
            throw new System.ArgumentNullException();

        int spawnPointIndex = UnityEngine.Random.Range(0, _spawnPoints.Length);
        Transform spawnPoint = _spawnPoints[spawnPointIndex];
        Enemy enemy = Instantiate(template, spawnPoint.position, Quaternion.identity, null);
        EnemySpawned?.Invoke(enemy);
    }
}