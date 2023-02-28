using System.Collections;
using UnityEngine;
using Agava.YandexGames;
using System;
using Lean.Localization;

public class Game : MonoBehaviour
{
    [SerializeField] private int _levelsBetweenAd = 3;

    [SerializeField] private Player _player;
    [SerializeField] private Level _level;
    [SerializeField] private float _secondsBetweenLevels;
    [SerializeField] private StoreMenu _store;
    [SerializeField] private Mana _mana;
    [SerializeField] private Wall _wall;
    [SerializeField] private EnemySpawner _enemySpawner;

    [SerializeField] private Score _score;

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private GameOverMenu _gameOverMenu;

    [SerializeField] private LevelMessage _levelMessage;

    [SerializeField] private RectTransform _howToPlayPanel;

    private static Game _instance;
    private AliveEnemiesHolder _aliveEnemiesHolder;

    public event Action LevelStarted;
    public event Action LevelCompleted;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    private IEnumerator Start()
    {
        _score.InitScore();
        enabled = false;
        _aliveEnemiesHolder = new AliveEnemiesHolder(_enemySpawner);
        _wall.Destroyed += OnWallDestroyed;
        _level.EnemySpawnFinished += () => enabled = true;

#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        while (YandexGamesSdk.IsInitialized == false)
            yield return new WaitForSeconds(0.5f);

        ShowStickyAd();

        if (PlayerAccount.HasPersonalProfileDataPermission == false)
            PlayerAccount.RequestPersonalProfileDataPermission();
    }

    private void OnDestroy()
    {
        _wall.Destroyed -= OnWallDestroyed;
    }

    private void Update()
    {
        if (_aliveEnemiesHolder.Count == 0)
        {
            CompleteLevel();
            StartCoroutine(StartNextLevelWithDelay(_secondsBetweenLevels));
        }
    }

    public static void Pause()
    {
        _instance._pauseMenu.Open();
        Time.timeScale = 0;
    }

    public static void Continue()
    {
        _instance._pauseMenu.Close();
        Time.timeScale = 1;
    }

    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();

        enabled = false;
        _level.AbortSpawn();
        _aliveEnemiesHolder.KillAllEnemies();

        _score.ResetScore();
        _store.Fill();
        _mana.ResetMana();
        _wall.ResetWall();

        StartLevel(0);

        _mainMenu.Close();
        _howToPlayPanel.gameObject.SetActive(true);
        Pause();
    }

    private void StartLevel(int level)
    {
        _level.StartLevel(level);
        _player.Play();
        string levelText = LeanLocalization.GetTranslationText("Level");
        _levelMessage.Show(levelText + " " + (_level.CurrentLevel + 1));
        LevelStarted?.Invoke();
        //HideStickyAd();
    }

    private IEnumerator StartNextLevelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_level.CurrentLevel % _levelsBetweenAd == 1)
            ShowInterstitialAd();

        StartLevel(_level.CurrentLevel + 1);
        Pause();
    }

    private void CompleteLevel()
    {
        _player.StopPlaying();
        enabled = false;
        string victoryText = LeanLocalization.GetTranslationText("Victory");
        _levelMessage.Show(victoryText + "!\n");
        LevelCompleted?.Invoke();
        //ShowStickyAd();
    }

    private void OnWallDestroyed()
    {
        string gameOverText = LeanLocalization.GetTranslationText("GameOver");
        _levelMessage.Show(gameOverText);
        _player.StopPlaying();
        _aliveEnemiesHolder.StopAllEnemies();
        StartCoroutine(ShowGameOverMenu());
    }

    private IEnumerator ShowGameOverMenu()
    {
        yield return new WaitForSeconds(_secondsBetweenLevels);
        _gameOverMenu.Open();
    }

    public void TryLevelAgain()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        RestartCurrentLevel();
        return;
#endif

        VideoAd.Show(
            onRewardedCallback: () => RestartCurrentLevel());
    }

    private void RestartCurrentLevel()
    {
        enabled = false;
        _level.AbortSpawn();
        _aliveEnemiesHolder.KillAllEnemies();
        StartLevel(_level.CurrentLevel);
        Pause();

        _wall.Repair(100);
        _gameOverMenu.Close();
        _mana.UndoLevelMana();
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

    private void ShowStickyAd()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
        StickyAd.Show();
    }

    private void HideStickyAd()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
        StickyAd.Hide();
    }
}