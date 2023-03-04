using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private ManaRenderer _manaRenderer;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Game _game;

    public ManaStorage ManaStorage { get; private set; }

    public void Start()
    {
        ManaStorage = new ManaStorage();
        _manaRenderer.Render(ManaStorage);
        _enemySpawner.EnemySpawned += OnEnemySpawned;
        _game.LevelCompleted += OnLevelFinished;
        
    }
    private void OnDestroy()
    {
        _enemySpawner.EnemySpawned -= OnEnemySpawned;
    }

    public void ResetMana()
    {
        ManaStorage.Reset();
    }

    public void UndoLevelMana()
    {
        ManaStorage.Load();
    }

    private void OnLevelFinished()
    {
        ManaStorage.Save();
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