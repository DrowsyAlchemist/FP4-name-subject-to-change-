using UnityEngine;


[System.Serializable]
public class EnemySequence
{
    [SerializeField] private Enemy _enemyTemplate;
    [SerializeField] private int _enemyCount;
    [SerializeField] private float _spawnDelay;

    public Enemy EnemyTemplate => _enemyTemplate;
    public int EnemyCount => _enemyCount;
    public float SpawnDelay => _spawnDelay;
}
