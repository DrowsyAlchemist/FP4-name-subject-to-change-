using UnityEngine;
using UnityEngine.UI;

public class SoundOnOffButton : UIButton
{
    [SerializeField] private Image _image;

    protected override void Start()
    {
        base.Start();
        _image.sprite = (Sound.IsOn) ? Sound.TurnedOnSprite : Sound.MuteSprite;
        Sound.ConditionChanged += OnSoundConditionChanged;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Sound.ConditionChanged -= OnSoundConditionChanged;
    }

    protected override void OnButtonClick()
    {
        if (Sound.IsOn)
        {
            Sound.Mute();
            _image.sprite = Sound.MuteSprite;
        }
        else
        {
            Sound.ClickSound.Play();
            Sound.TurnOn();
            _image.sprite = Sound.TurnedOnSprite;
        }
    }

    private void OnSoundConditionChanged(bool isOn)
    {
        if (Sound.IsOn)
            _image.sprite = Sound.TurnedOnSprite;
        else
            _image.sprite = Sound.MuteSprite;
    }
}