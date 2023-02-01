using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSetup", menuName = "ScriptableObjects/LevelSetup", order = 51)]
public class LevelSetup : ScriptableObject, ISpawnSequence
{
    [SerializeField] private float _secondsBetweenWaves;
    [SerializeField] private List<EnemyWave> _waves;

    public int Count => _waves.Count;
    public float SpawnDelay => _secondsBetweenWaves;

    public EnemyWave GetWave(int index)
    {
        return _waves[index];
    }
}
