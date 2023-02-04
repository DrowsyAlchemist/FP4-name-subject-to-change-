using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private LevelSetup _levelSetup;

    public void StartLevel()
    {
        _levelSpawner.StartSpawn(_levelSetup);
        _levelSpawner.SequenceFinished += OnLevelFinished;
    }

    private void OnDestroy()
    {
        _levelSpawner.SequenceFinished -= OnLevelFinished;
    }

    private void OnLevelFinished()
    {
        Debug.Log("Level finished.");
    }
}
