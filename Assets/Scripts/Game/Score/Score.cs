using System;
using UnityEngine;
using Agava.YandexGames;

public class Score : MonoBehaviour
{
    [SerializeField] private LeaderboardMenu _leaderboardMenu;
    [SerializeField] private Game _game;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Wall _wall;
    [SerializeField] private SDKLoader _sdkLoader;

    private const string LeaderboardName = "MagicWallLeaderboard";
    private ScoreCounter _counter;
    private int _bestScore;

    public int BestScore
    {
        get
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            return PlayerPrefs.GetInt(LeaderboardName);
#endif
            if (YandexGamesSdk.IsInitialized && PlayerAccount.IsAuthorized)
                Leaderboard.GetPlayerEntry(LeaderboardName, (entry) => _bestScore = entry.score);
            else
                return PlayerPrefs.GetInt(LeaderboardName);

            return _bestScore;
        }
    }

    public int CurrentScore => _counter.Score;

    public event Action<int> CurrentScoreChanged;
    public event Action<int> BestScoreChanged;

    private void Start()
    {
        _leaderboardMenu.Authorized += () => BestScoreChanged?.Invoke(BestScore);
    }

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
            if (PlayerAccount.IsAuthorized)
                Leaderboard.SetScore(LeaderboardName, score);
            else
                PlayerPrefs.SetInt(LeaderboardName, score);
#endif
            BestScoreChanged?.Invoke(score);
        }
        CurrentScoreChanged?.Invoke(score);
    }
}