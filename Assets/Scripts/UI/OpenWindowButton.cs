using UnityEngine;

public class OpenWindowButton : UIButton
{
    [SerializeField] private RectTransform _window;

    protected override void OnButtonClick()
    {
        _window.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}