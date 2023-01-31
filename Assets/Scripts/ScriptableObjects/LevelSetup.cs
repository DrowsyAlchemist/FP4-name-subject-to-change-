using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSetup", menuName = "ScriptableObjects/LevelSetup", order = 51)]
public class LevelSetup : ScriptableObject
{
    [SerializeField] private float _secondsBetweenWaves;
    [SerializeField] private List<Wave> _waves;

    public float SecondsBetweenWaves => _secondsBetweenWaves;
    public int WavesCount => _waves.Count;

    public Wave GetWave(int index)
    {
        return _waves[index];
    }
}
