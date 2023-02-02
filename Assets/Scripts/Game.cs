using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Store _store;
    [SerializeField] private LevelSpawner _level;
    [SerializeField] private LevelSetup _levelSetup;

    [SerializeField] private Wall _wall;

    [SerializeField] private ManaRenderer _manaRenderer;

    [SerializeField] private EnemySpawner _enemySpawner;

    private static Game _instance;

    public static Wall Wall => _instance._wall;
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
            _manaRenderer.Render(ManaStorage);
            _enemySpawner.EnemySpawned += OnEnemySpawned;
        }
    }

    private void Start()
    {
        _player.Play();
        _level.StartLevel(_levelSetup);
        _level.SequenceFinished += OnLevelFinished;
    }

    private void OnDestroy()
    {
        _level.SequenceFinished -= OnLevelFinished;
        _enemySpawner.EnemySpawned -= OnEnemySpawned;
    }

    private void OnLevelFinished()
    {
        Debug.Log("Level finished.");
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        enemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;
        ManaStorage.TakeMana(enemy.Reward);
    }
}