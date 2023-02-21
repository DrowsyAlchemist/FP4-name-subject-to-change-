using System;

public class ScoreCounter
{
    private const float EnemyRewardModifier = 2.1f;
    private const float WallHealthModifier = 0.1f;
    private readonly Game _game;
    private readonly EnemySpawner _enemySpawner;
    private readonly Wall _wall;
    private int _currentLevelScore;

    public int Score { get; private set; }

    public event Action <int> ScoreChanged;

    public ScoreCounter(Game game, EnemySpawner enemySpawner, Wall wall)
    {
        if (game == null || enemySpawner == null || wall == null)
            throw new ArgumentNullException();

        _game = game;
        _enemySpawner = enemySpawner;
        _wall = wall;
        game.LevelFinished += OnLevelFinished;
        enemySpawner.EnemySpawned += OnEnemySpawned;
    }

    ~ScoreCounter()
    {
        _game.LevelFinished -= OnLevelFinished;
        _enemySpawner.EnemySpawned -= OnEnemySpawned;
    }

    public void Reset()
    {
        Score = 0;
    }

    private void OnLevelFinished()
    {
        _currentLevelScore = (int)(_currentLevelScore * EnemyRewardModifier);
        int scoreDecrease = (int)((_wall.Health.MaxHealth - _wall.Health.CurrentHealth) * WallHealthModifier);
        scoreDecrease = Math.Clamp(scoreDecrease, 0, _currentLevelScore);
        Score += _currentLevelScore - scoreDecrease;
        _currentLevelScore = 0;
        ScoreChanged?.Invoke(Score);
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        enemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;
        _currentLevelScore += enemy.Reward;
    }
}