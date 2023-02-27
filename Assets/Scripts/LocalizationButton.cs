using Lean.Localization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationButton : UIButton
{
    [SerializeField] private Image _image;
    [SerializeField] private List<Language> _languages = new List<Language>();

    private int _currentLanguage;

    protected override void Start()
    {
        base.Start();
        Render(_currentLanguage);
    }

    protected override void OnButtonClick()
    {
        _currentLanguage++;

        if (_currentLanguage == _languages.Count)
            _currentLanguage = 0;

        Render(_currentLanguage);
    }

    private void Render(int language)
    {
        LeanLocalization.SetCurrentLanguageAll(_languages[language].Name);
        _image.sprite = _languages[language].Sprite;
    }

    [System.Serializable]
    private class Language
    {
        public string Name;
        public Sprite Sprite;
    }
}
