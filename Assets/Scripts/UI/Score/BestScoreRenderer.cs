using Lean.Localization;
using TMPro;
using UnityEngine;

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
        OnScoreChanged(_score.BestScore);
    }

    private void OnScoreChanged(int score)
    {
        string textBeforeScore = LeanLocalization.GetTranslationText(_textBeforeScore);

        string scoreText = (score > 0) ? score.ToString() : "???";
        _bestScoreText.text = textBeforeScore + scoreText;
    }
}
