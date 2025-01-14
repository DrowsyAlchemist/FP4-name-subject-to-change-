using UnityEngine;


[System.Serializable]
public class EnemyOrder : ISpawnSequence
{
    [SerializeField] private Enemy _enemyTemplate;
    [SerializeField] private bool _hasShield;
    [SerializeField] private int _enemyCount;
    [SerializeField] private float _spawnDelay;

    public Enemy EnemyTemplate => _enemyTemplate;
    public bool HasShield => _hasShield;    
    public int Count => _enemyCount;
    public float SpawnDelay => _spawnDelay;
}
