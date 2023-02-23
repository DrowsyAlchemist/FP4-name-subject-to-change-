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

    [SerializeField] private MenuWindow _menuWindow;

    [SerializeField] private LevelMessage _levelMessage;

    [SerializeField] private RectTransform _pauseMenu;

    [SerializeField] private RectTransform _gameOverMenu;

    private static Game _instance;
    private AliveEnemiesHolder _aliveEnemiesHolder;

    public static Wall Wall => _instance._wall;

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
        _score.ResetScore(); ;
        enabled = false;
        _menuWindow.PlayButtonClicked += StartGame;
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
        _menuWindow.Close();
        _store.Fill();
        _mana.Init(_enemySpawner);
        _player.Play();
        _wall.Destroyed += OnWallDestroyed;
        _aliveEnemiesHolder = new AliveEnemiesHolder(_enemySpawner);
        _enemySpawner.AllowSpawning();
        _level.EnemySpawnFinished += () => enabled = true;
        StartNextLevel();
    }

    private void StartNextLevel()
    {
        _player.Play();
        _level.StartNextLevel();
        _levelMessage.Show("Level " + (_level.CurrentLevel + 1));
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
            RestartLevel();
            return;
#endif

            VideoAd.Show(
                onRewardedCallback: () => RestartLevel());
    }

    private void RestartLevel()
    {
        try
        {
            _level.StartLevel(_level.CurrentLevel);
            _aliveEnemiesHolder.KillAllEnemies();
            _wall.Repair(100);
            _gameOverMenu.gameObject.SetActive(false);
            _player.Play();
            _levelMessage.Show("Level " + (_level.CurrentLevel + 1));
        }
        catch (Exception)
        {
            Debug.Log("Error!");
        }
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