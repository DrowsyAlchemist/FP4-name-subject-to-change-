using Agava.YandexGames;
using Lean.Localization;
using System.Collections;
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
        StartCoroutine(SetEnvironmentLanguage());
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
        LeanLocalization.SetCurrentLanguageAll(_languages[_currentLanguage].Name);
        _image.sprite = _languages[language].Sprite;
    }

    private IEnumerator SetEnvironmentLanguage()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        while (YandexGamesSdk.IsInitialized == false)
            yield return null;

            string lang = YandexGamesSdk.Environment.i18n.lang;
#else
        yield return null;
        string lang = "ru";
#endif
        foreach (var language in _languages)
            if (language.Code == lang)
                _currentLanguage = _languages.IndexOf(language);

        Render(_currentLanguage);
    }

    [System.Serializable]
    private class Language
    {
        public string Name;
        public string Code;
        public Sprite Sprite;
    }
}