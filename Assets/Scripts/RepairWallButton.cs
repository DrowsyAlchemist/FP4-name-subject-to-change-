using UnityEngine;
using Agava.YandexGames;
using UnityEngine.UI;

public class RepairWallButton : UIButton
{
    [SerializeField] private Game _game;
    [SerializeField] private Wall _wall;

    private const float _restorePercents = 100;

    private void Awake()
    {
        _game.LevelFinished += () => Button.interactable = true;
    }

    protected override void OnButtonClick()
    {
        Button.interactable = false;

#if !UNITY_WEBGL || UNITY_EDITOR
        _wall.Repair(_restorePercents);
        return;
#endif

        VideoAd.Show(
            onOpenCallback: () => Time.timeScale = 0,
            onCloseCallback: () => Time.timeScale = 1,
            onRewardedCallback: () => _wall.Repair(_restorePercents));
    }
}