using System;
using UnityEngine;
using Agava.YandexGames;

public class Score : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Wall _wall;

    private const string LeaderboardName = "MagicWallLeaderboard";
    private ScoreCounter _counter;

    public int BestScore
    {
        get
        {
#if !UNITY_WEBGL || UNITY_EDITOR
           return PlayerPrefs.GetInt(LeaderboardName);
#else
            int score = 0;
            Leaderboard.GetEntries(LeaderboardName, (result) => score = result.userRank);
            return score;
#endif
        }
    }
    public int CurrentScore => _counter.Score;

    public event Action<int> CurrentScoreChanged;
    public event Action<int> BestScoreChanged;

    private void OnDestroy()
    {
        _counter.ScoreChanged -= OnScoreChanged;
    }

    public void InitScore()
    {
        if (_counter == null)
        {
            _counter = new ScoreCounter(_game, _enemySpawner, _wall);
            _counter.ScoreChanged += OnScoreChanged;
        }
    }

    public void ResetScore()
    {
        _counter.Reset();
    }

    private void OnScoreChanged(int score)
    {
        if (BestScore < score)
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            PlayerPrefs.SetInt(LeaderboardName, score);
#else
            Leaderboard.SetScore(LeaderboardName, score);
#endif

            BestScoreChanged?.Invoke(score);
        }
        CurrentScoreChanged?.Invoke(score);
    }
}