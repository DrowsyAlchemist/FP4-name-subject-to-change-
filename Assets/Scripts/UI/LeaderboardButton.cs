using TMPro;
using UnityEngine;
using Agava.YandexGames;

public class LeaderboardButton : OpenWindowButton
{
    [SerializeField] private string _leaderboardName = "MagicWallLeaderboard";
    [SerializeField] private TMP_Text _text;

    protected override void OnButtonClick()
    {
        base.OnButtonClick();

#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif

        Leaderboard.GetEntries(_leaderboardName, (result) =>
        {
            _text.text = $"My rank = {result.userRank} \n";

            foreach (var entry in result.entries)
            {
                string name = entry.player.publicName;
                if (string.IsNullOrEmpty(name))
                    name = "Anonymous";
                _text.text += name + " " + entry.score + "\n";
            }
        });
    }
}