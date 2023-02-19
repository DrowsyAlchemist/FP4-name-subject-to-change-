using UnityEngine;
using Agava.YandexGames;
using UnityEngine.UI;

public class WallRepairer : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Button _button;
    [SerializeField] private RectTransform _highlightedFrame;

    private const float _restorePercents = 100;

    private void Start()
    {
        _game.LevelFinished += () => SetActive(true);
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        SetActive(false);

#if !UNITY_WEBGL || UNITY_EDITOR
        Game.Wall.RestoreHealth(_restorePercents);
        return;
#endif

        VideoAd.Show(
            onOpenCallback: () => Time.timeScale = 0,
            onCloseCallback: () => Time.timeScale = 1,
            onRewardedCallback: () => Game.Wall.RestoreHealth(_restorePercents));
    }

    private void SetActive(bool isActive)
    {
        _button.interactable = isActive;
        _highlightedFrame.gameObject.SetActive(isActive);
    }
}