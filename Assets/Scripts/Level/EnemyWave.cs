using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave : ISpawnSequence
{
    [SerializeField] private float _secondsBetweenSequences;
    [SerializeField] private List<EnemyOrder> _enemySequences;

    public int Count => _enemySequences.Count;
    public float SpawnDelay => _secondsBetweenSequences;

    public EnemyOrder GetSequence(int index)
    {
        return _enemySequences[index];
    }
}