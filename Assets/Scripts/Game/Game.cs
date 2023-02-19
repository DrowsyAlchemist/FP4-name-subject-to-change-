using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Agava.YandexGames;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Level _level;
    [SerializeField] private float _secondsBetweenLevels;
    [SerializeField] private StoreMenu _store;
    [SerializeField] private Mana _mana;
    [SerializeField] private Wall _wall;
    [SerializeField] private EnemySpawner _enemySpawner;

    [SerializeField] private MenuWindow _menuWindow;

    [SerializeField] private LevelMessage _levelMessage;

    private static Game _instance;
    private AliveEnemiesHolder _aliveEnemiesHolder;

    public static Wall Wall => _instance._wall;

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        StartCoroutine(InitYandexSDK());
        enabled = false;
        _menuWindow.PlayButtonClicked += StartGame;
    }

    private void Update()
    {
        if (_aliveEnemiesHolder.Count == 0)
        {
            _levelMessage.Show("Victory!");
            _player.StopPlaying();
            _wall.Upgrade();
            enabled = false;
            StartCoroutine(StartNextLevelWithDelay(_secondsBetweenLevels));
        }
    }

    private void StartGame()
    {
        _menuWindow.Close();
        PlayerPrefs.DeleteAll();
        _store.Fill();
        _mana.Init(_enemySpawner);
        _player.Play();
        _wall.Destroyed += OnWallDestroyed;
        _aliveEnemiesHolder = new AliveEnemiesHolder(_enemySpawner);
        _enemySpawner.AllowSpawning();
        _level.LevelFinished += () => enabled = true;
        StartCoroutine(StartNextLevelWithDelay(0));
    }

    private IEnumerator StartNextLevelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _player.Play();
        _level.StartNextLevel();
        _levelMessage.Show("Level " + (_level.CurrentLevel + 1));

#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        if (_level.CurrentLevel % 2 == 1)
            InterstitialAd.Show(onOpenCallback: () => Time.timeScale = 0, onCloseCallback: (_) => Time.timeScale = 1);
    }

    private void OnWallDestroyed()
    {
        _wall.Destroyed -= OnWallDestroyed;
        _levelMessage.Show("GameOver");
        _player.StopPlaying();
        _enemySpawner.StopSpawning();
        _aliveEnemiesHolder.StopAllEnemies();
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);

#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        VideoAd.Show(onOpenCallback: () => Time.timeScale = 0, onCloseCallback: () => Time.timeScale = 1);
    }

    private IEnumerator InitYandexSDK()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        while (YandexGamesSdk.IsInitialized == false)
            yield return YandexGamesSdk.Initialize();
    }
}