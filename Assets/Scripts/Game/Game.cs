using System.Collections;
using UnityEngine;
using Agava.YandexGames;
using System;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Level _level;
    [SerializeField] private float _secondsBetweenLevels;
    [SerializeField] private StoreMenu _store;
    [SerializeField] private Mana _mana;
    [SerializeField] private Wall _wall;
    [SerializeField] private EnemySpawner _enemySpawner;

    [SerializeField] private Score _score;

    [SerializeField] private MainMenu _menuWindow;

    [SerializeField] private LevelMessage _levelMessage;

    [SerializeField] private RectTransform _pauseMenu;

    [SerializeField] private RectTransform _gameOverMenu;

    private static Game _instance;
    private AliveEnemiesHolder _aliveEnemiesHolder;

    public static Wall Wall => _instance._wall;

    public event Action LevelStarted;
    public event Action LevelFinished;

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;

        _score.InitScore();
    }

    private void Start()
    {
        StartCoroutine(InitYandexSDK());
        enabled = false;
        _menuWindow.PlayButtonClicked += StartGame;
        _aliveEnemiesHolder = new AliveEnemiesHolder(_enemySpawner);
    }

    private void Update()
    {
        if (_aliveEnemiesHolder.Count == 0)
        {
            _player.StopPlaying();
            enabled = false;
            LevelFinished?.Invoke();
            _levelMessage.Show("Victory!\nScore: " + _score.CurrentScore);
            StartCoroutine(StartNextLevelWithDelay(_secondsBetweenLevels));
        }
    }

    public void OpenPauseMenu()
    {
        _pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void StartGame()
    {
        PlayerPrefs.DeleteAll(); /////
        enabled = false;
        _menuWindow.Close();
        _score.ResetScore();
        _store.Fill();
        _wall.Destroyed += OnWallDestroyed;
        _aliveEnemiesHolder.KillAllEnemies();
        _mana.ResetMana();

        _enemySpawner.AllowSpawning();
        _level.EnemySpawnFinished += () => enabled = true;



        _level.AbortSpawn();
        _level.StartLevel(0);
        _wall.Repair(100);
        _gameOverMenu.gameObject.SetActive(false);
        _player.Play();
        _levelMessage.Show("Level " + (_level.CurrentLevel + 1));
        LevelStarted?.Invoke();
    }

    private void StartNextLevel()
    {
        _player.Play();
        _level.StartNextLevel();
        _levelMessage.Show("Level " + (_level.CurrentLevel + 1));
        LevelStarted?.Invoke();
    }

    private IEnumerator StartNextLevelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_level.CurrentLevel % 2 == 1)
            ShowInterstitialAd();

        OpenPauseMenu();
        StartNextLevel();
    }

    private void OnWallDestroyed()
    {
        _levelMessage.Show("GameOver");
        _player.StopPlaying();
        _aliveEnemiesHolder.StopAllEnemies();
        StartCoroutine(ShowGameOverMenu());
    }

    private IEnumerator ShowGameOverMenu()
    {
        yield return new WaitForSeconds(_secondsBetweenLevels);
        _gameOverMenu.gameObject.SetActive(true);
    }

    public void TryLevelAgain()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        RestartLevel(_level.CurrentLevel);
        return;
#endif

        VideoAd.Show(
            onRewardedCallback: () => RestartLevel(_level.CurrentLevel));
    }

    private void RestartLevel(int level)
    {
        enabled = false;
        _level.AbortSpawn();
        _level.StartLevel(level);
        _aliveEnemiesHolder.KillAllEnemies();
        _wall.Repair(100);
        _gameOverMenu.gameObject.SetActive(false);
        _player.Play();
        _levelMessage.Show("Level " + (_level.CurrentLevel + 1));
        LevelStarted?.Invoke();
        _mana.UndoLevelMana();
        Time.timeScale = 0;
        _pauseMenu.gameObject.SetActive(true);
    }

    private IEnumerator InitYandexSDK()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        while (YandexGamesSdk.IsInitialized == false)
            yield return YandexGamesSdk.Initialize();
    }

    private void ShowInterstitialAd()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
        InterstitialAd.Show(onOpenCallback: () => Time.timeScale = 0);
    }
}