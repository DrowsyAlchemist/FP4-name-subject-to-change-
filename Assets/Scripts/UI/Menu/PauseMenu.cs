using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private RectTransform _pauseWindow;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _backToMenuButton;

    private void Start()
    {
        _continueButton.onClick.AddListener(OnContinueButtonClick);
        _backToMenuButton.onClick.AddListener(OnBackToMenuButtonClick);
    }

    private void OnDestroy()
    {
        _continueButton.onClick.RemoveListener(OnContinueButtonClick);
        _backToMenuButton.onClick.RemoveListener(OnBackToMenuButtonClick);
    }

    public void Open()
    {
        _pauseWindow.gameObject.SetActive(true);
    }

    public void Close()
    {
        _pauseWindow.gameObject.SetActive(false);
    }

    private void OnContinueButtonClick()
    {
        Game.Continue();
    }

    private void OnBackToMenuButtonClick()
    {
        Close();
        _mainMenu.Open();
    }
}
