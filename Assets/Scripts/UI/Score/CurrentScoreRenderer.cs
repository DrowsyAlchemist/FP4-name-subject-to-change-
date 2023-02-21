using TMPro;
using UnityEngine;

public class CurrentScoreRenderer : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private string _textBeforeScore;

    private void Start()
    {
        _score.CurrentScoreChanged += OnScoreChanged;
        OnScoreChanged(_score.CurrentScore);
    }

    private void OnDestroy()
    {
        _score.CurrentScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _currentScoreText.text = _textBeforeScore + score.ToString();
    }
}