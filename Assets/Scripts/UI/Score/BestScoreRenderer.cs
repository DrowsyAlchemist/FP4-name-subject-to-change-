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
        OnScoreChanged(_score.BestScore);
    }

    private void OnDestroy()
    {
        _score.BestScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _bestScoreText.text = _textBeforeScore + score.ToString();
    }
}
