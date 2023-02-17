using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuWindow : MonoBehaviour
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
        _window.gameObject.SetActive(true);
    }

    public void Close()
    {
        _window.gameObject.SetActive(false);
    }

    private void OnPlayButtonClicked()
    {
        PlayButtonClicked?.Invoke();
    }
}