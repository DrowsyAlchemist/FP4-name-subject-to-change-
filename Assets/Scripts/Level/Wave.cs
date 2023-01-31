using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField] private float _secondsBetweenSequences;
    [SerializeField] private List<EnemySequence> _enemySequences;

    public float SecondsBetweenSequences => _secondsBetweenSequences;
    public int SequenceCount => _enemySequences.Count;

    public EnemySequence GetSequence(int index)
    {
        return _enemySequences[index];
    }
}