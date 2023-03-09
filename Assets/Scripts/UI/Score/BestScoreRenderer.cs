using Lean.Localization;
using System.Collections;
using TMPro;
using UnityEngine;
using Agava.YandexGames;

public class BestScoreRenderer : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private string _textBeforeScore;

    private void Start()
    {
        _score.BestScoreChanged += OnScoreChanged;
    }

    private void OnDestroy()
    {
        _score.BestScoreChanged -= OnScoreChanged;
    }

    private void OnEnable()
    {
        StartCoroutine(UpdateAfterInitialization());
    }

    private IEnumerator UpdateAfterInitialization()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        OnScoreChanged(_score.BestScore);
        yield break;
#endif
        while (YandexGamesSdk.IsInitialized == false)
            yield return null;

        OnScoreChanged(_score.BestScore);
    }

    private void OnScoreChanged(int score)
    {
        string textBeforeScore = LeanLocalization.GetTranslationText(_textBeforeScore);
        _bestScoreText.text = textBeforeScore + score.ToString();
    }
}
