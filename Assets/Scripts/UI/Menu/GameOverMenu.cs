using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : Menu
{
    [SerializeField] private Game _game;
    [SerializeField] private MainMenu _mainMenu;    
    [SerializeField] private Button _increaseMaxHealthButton;
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private Button _backToMenuButton;

    private void Start()
    {
        _tryAgainButton.onClick.AddListener(OnTryAgainButtonClick);
        _backToMenuButton.onClick.AddListener(OnBackToMenuButtonClick);
    }

    private void OnDestroy()
    {
        _tryAgainButton.onClick.RemoveListener(OnTryAgainButtonClick);
        _backToMenuButton.onClick.RemoveListener(OnBackToMenuButtonClick);
    }

    private void OnTryAgainButtonClick()
    {
        _game.TryLevelAgain();
        _increaseMaxHealthButton.interactable = true;
    }

    private void OnBackToMenuButtonClick()
    {
        _mainMenu.Open();
    }
}
