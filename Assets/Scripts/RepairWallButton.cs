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
        _game.LevelCompleted += () => Button.interactable = true;
        _wall.Destroyed += () => Button.interactable = false;
    }

    protected override void OnButtonClick()
    {
        Button.interactable = false;

#if !UNITY_WEBGL || UNITY_EDITOR
        _wall.Repair(_restorePercents);
        Game.Pause();
        return;
#endif
        VideoAd.Show(
            onOpenCallback: () =>
            {
                Game.Pause();
                Sound.BackgroundMusic.Stop();
            },
            onRewardedCallback: () => _wall.Repair(_restorePercents),
            onCloseCallback: () => Sound.BackgroundMusic.Play()
            );
    }
}