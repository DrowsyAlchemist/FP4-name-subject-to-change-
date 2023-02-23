using UnityEngine;

public class NextLevelButton : UIButton
{
    [SerializeField] private RectTransform _pausePanel;

    protected override void OnButtonClick()
    {
        _pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
