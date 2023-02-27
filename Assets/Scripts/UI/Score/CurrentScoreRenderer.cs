using TMPro;
using UnityEngine;
using Lean.Localization;

public class CurrentScoreRenderer : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private string _textBeforeScore;

    private void Start()
    {
        _score.CurrentScoreChanged += OnScoreChanged;
    }

    private void OnDestroy()
    {
        _score.CurrentScoreChanged -= OnScoreChanged;
    }

    private void OnEnable()
    {
        OnScoreChanged(_score.CurrentScore);
    }

    private void OnScoreChanged(int score)
    {
        string textBeforeScore = LeanLocalization.GetTranslationText(_textBeforeScore);
        _currentScoreText.text = textBeforeScore + score.ToString();
    }
}