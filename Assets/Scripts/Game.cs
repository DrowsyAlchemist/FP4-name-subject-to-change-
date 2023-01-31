using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Store _store;
    [SerializeField] private Level _level;

    private static Game _instance;

    public ManaStorage ManaStorage { get; private set; }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            ManaStorage = new ManaStorage();
        }
    }

    private void Start()
    {
        _player.Play();
        _level.LevelFinished += OnLevelFinished;
        _level.StartNextWave();
    }

    private void OnDestroy()
    {
        _level.LevelFinished -= OnLevelFinished;
    }

    private void OnLevelFinished()
    {
        Debug.Log("Level finished.");
    }
}