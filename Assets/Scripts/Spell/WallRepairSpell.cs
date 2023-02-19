using UnityEngine;
using Agava.YandexGames;

public class WallRepairSpell : UpgradeableSpell
{
    [SerializeField] private float _restorePercents;

    public void Use()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif

        VideoAd.Show(
            onOpenCallback: () => Time.timeScale = 0,
            onCloseCallback: () => Time.timeScale = 1,
            onRewardedCallback: () => Game.Wall.RestoreHealth(_restorePercents));
    }
}