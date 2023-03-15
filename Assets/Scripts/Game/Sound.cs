using UnityEngine;
using UnityEngine.Audio;
using Agava.WebUtility;
using System;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _masterVolumeName;
    [SerializeField] private float _maxValue = 0;
    [SerializeField] private float _minValue = -80;

    [SerializeField] private Sprite _turnedOnSprite;
    [SerializeField] private Sprite _muteSprite;

    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private AudioSource _clickSound;
    [SerializeField] private AudioSource _sellSound;
    [SerializeField] private AudioSource _spellHitSound;
    [SerializeField] private AudioSource _enemyDeadSound;
    [SerializeField] private AudioSource _levelCompletedSound;

    private static Sound _instance;

    public static bool IsOn { get; private set; }
    public static Sprite TurnedOnSprite => _instance._turnedOnSprite;
    public static Sprite MuteSprite => _instance._muteSprite;
    public static AudioSource BackgroundMusic => _instance._backgroundMusic;
    public static AudioSource ClickSound => _instance._clickSound;
    public static AudioSource SellSound => _instance._sellSound;
    public static AudioSource SpellHitSound => _instance._spellHitSound;
    public static AudioSource EnemyDeadSound => _instance._enemyDeadSound;
    public static AudioSource LevelCompletedSound => _instance._levelCompletedSound;

    public static event Action<bool> ConditionChanged;

    private void Awake()
    {
        if (_instance == false)
            _instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Mute();
        WebApplication.InBackgroundChangeEvent += OnBackgroundChanged;
        ConditionChanged.Invoke(false);
    }

    private void OnDestroy()
    {
        WebApplication.InBackgroundChangeEvent -= OnBackgroundChanged;
    }

    public static void TurnOn()
    {
        _instance.TurnSoundOn();
        IsOn = true;
        ConditionChanged.Invoke(true);
    }

    public static void Mute()
    {
        _instance.TurnSoundOff();
        IsOn = false;
        ConditionChanged.Invoke(false);
    }

    private void OnBackgroundChanged(bool isOut)
    {
        if (isOut)
        {
            TurnSoundOff();
        }
        else
        {
            if (IsOn)
                TurnSoundOn();
        }
    }

    private void TurnSoundOn()
    {
        _instance._mixer.SetFloat(_instance._masterVolumeName, _instance._maxValue);
    }

    private void TurnSoundOff()
    {
        _instance._mixer.SetFloat(_instance._masterVolumeName, _instance._minValue);
    }
}