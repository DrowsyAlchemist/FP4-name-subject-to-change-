using System.Collections;
using UnityEngine;
using Agava.YandexGames;
using System;
using Lean.Localization;

public class Game : MonoBehaviour
{
    [SerializeField] private int _adStartLevel = 2;
    [SerializeField] private int _levelsBetweenAd = 3;

    [SerializeField] private Player _player;
    [SerializeField] private Wall _wall;
    [SerializeField] private Mana _mana;
    [SerializeField] private StoreMenu _store;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Score _score;
    [SerializeField] private Level _level;
    [SerializeField] private float _secondsBetweenLevels;

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private GameOverMenu _gameOverMenu;
    [SerializeField] private HowToPlayMenu _howToPlayMenu;

    [SerializeField] private GameMessage _gameMessage;


    private const string LevelPhrase = "Level";
    private const string VictoryPhrase = "Victory";
    private const string GameOverPhrase = "GameOver";
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

        if (PlayerAccount.HasPersonalProfileDataPermission == false)
            PlayerAccount.RequestPersonalProfileDataPermission();
    }

    private void OnDestroy()
    {
        _wall.Destroyed -= OnWallDestroyed;
    }

    private void FixedUpdate()
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
        _mana.ResetMana();
        _wall.ResetWall();
        _store.Fill();

        StartLevel(0);

        _mainMenu.Close();
        _howToPlayMenu.Open();
        Pause();
    }

    private void StartLevel(int level)
    {
        _level.StartLevel(level);
        _player.Play();
        string levelText = LeanLocalization.GetTranslationText(LevelPhrase);
        _gameMessage.Show(levelText + " " + (_level.CurrentLevel + 1));
        LevelStarted?.Invoke();
    }

    private IEnumerator StartNextLevelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_level.CurrentLevel % _levelsBetweenAd == _adStartLevel)
            ShowInterstitialAd();

        StartLevel(_level.CurrentLevel + 1);
        Pause();
    }

    private void CompleteLevel()
    {
        _player.StopPlaying();
        enabled = false;
        string victoryText = LeanLocalization.GetTranslationText(VictoryPhrase);
        Sound.LevelCompletedSound.Play();
        _gameMessage.Show(victoryText + "!\n");
        LevelCompleted?.Invoke();

        #region
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
        if (PlayerAccount.IsAuthorized)
            PlayerAccount.GetProfileData(
                onSuccessCallback: (data) =>
                {
                    Debug.Log("///////////////////////////////////\n" +
                        "ID: " + data.uniqueID + "\n" +
                        "////////////////////////////////////\n");

                    if (data.uniqueID == "kAnv7Obvztit9udo49V0rbsx/CvtHxCiwOnQ0pDNs/k=")
                        //_levelMessage.Show("Люблю моллюска <3");
                        _gameMessage.Show("Победа!!!");
                });
        #endregion
    }

    private void OnWallDestroyed()
    {
        string gameOverText = LeanLocalization.GetTranslationText(GameOverPhrase);
        _gameMessage.Show(gameOverText);
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
            onOpenCallback: () => Sound.BackgroundMusic.Stop(),
            onRewardedCallback: () => RestartCurrentLevel(),
            onCloseCallback: () => Sound.BackgroundMusic.Play()
            );
    }

    private void RestartCurrentLevel()
    {
        enabled = false;
        _level.AbortSpawn();
        _aliveEnemiesHolder.KillAllEnemies();
        _wall.Repair(100);
        _mana.UndoLevelMana();

        StartLevel(_level.CurrentLevel);
        _gameOverMenu.Close();
        Pause();
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