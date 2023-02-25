using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private RectTransform _window;
    [SerializeField] private Button _playButton;

    public event Action PlayButtonClicked;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClicked);
    }

    public void Open()
    {
        SceneManager.LoadScene(0);
    }

    public void Close()
    {
        _window.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnPlayButtonClicked()
    {
        PlayButtonClicked?.Invoke();
    }
}