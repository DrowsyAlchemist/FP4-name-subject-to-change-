using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
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

    public void Open()
    {
        _window.gameObject.SetActive(true);
    }

    public void Close()
    {
        _window.gameObject.SetActive(false);   
    }

    private void OnPlayButtonClicked()
    {
        _game.StartNewGame();
    }
}