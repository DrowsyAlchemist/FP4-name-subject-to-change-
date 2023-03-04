using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [SerializeField] private Game _game;
    [SerializeField] private RectTransform _window;
    [SerializeField] private Button _playButton;

    private void Start()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        _game.StartNewGame();
    }
}