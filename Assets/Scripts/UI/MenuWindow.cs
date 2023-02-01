using UnityEngine;
using UnityEngine.UI;

public class MenuWindow : MonoBehaviour
{
    [SerializeField] private RectTransform _window;
    [SerializeField] private Button _playButton;

    public void Open()
    {
        _window.gameObject.SetActive(true);
    }

    public void Close()
    {
        _window.gameObject.SetActive(false);
    }
}
