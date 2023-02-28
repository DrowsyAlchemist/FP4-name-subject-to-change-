using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames;

public class LeaderboardEntryRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _rankText;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _scoreText;

    public void Render(LeaderboardEntryResponse entry)
    {
        _rankText.text = entry.rank.ToString();
        _nameText.text = GetName(entry);
        _scoreText.text = entry.score.ToString();
    }

    private string GetName(LeaderboardEntryResponse entry)
    {
        string name = entry.player.publicName;

        if (string.IsNullOrEmpty(name))
            name = "Anonymous";

        return name;
    }
}
