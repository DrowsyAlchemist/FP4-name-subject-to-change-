using UnityEngine;
using Agava.YandexGames;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class LeaderboardMenu : MonoBehaviour
{
    [SerializeField] private string _leaderboardName = "MagicWallLeaderboard";
    [SerializeField] private RectTransform _entriesContainer;
    [SerializeField] private LeaderboardEntryRenderer _playerRntryRenderer;
    [SerializeField] private LeaderboardEntryRenderer _entryRendererTemplate;
    [SerializeField] private Button _logInButton;

    private List<LeaderboardEntryRenderer> _entries = new ();

    public event Action Authorized;

    private void Start()
    {
        _logInButton.onClick.AddListener(Authorize);
    }

    private void OnDestroy()
    {
        _logInButton.onClick.RemoveListener(Authorize);
    }

    private void OnEnable()
    {
        _logInButton.interactable = false;

#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
        if (YandexGamesSdk.IsInitialized)
        {
            UpdateLeaderboard();
            _logInButton.interactable = PlayerAccount.IsAuthorized == false;
        }
    }

    private void UpdateLeaderboard()
    {
        Clear();
        Leaderboard.GetPlayerEntry(_leaderboardName, (result) => _playerRntryRenderer.Render(result));
        Leaderboard.GetEntries(_leaderboardName, (result) =>
        {
            foreach (var entry in result.entries)
            {
                var entryRenderer = Instantiate(_entryRendererTemplate, _entriesContainer);
                entryRenderer.Render(entry);
                _entries.Add(entryRenderer);
            }
        });
    }

    private void Clear()
    {
        while (_entries.Count > 0)
        {
            Destroy(_entries[0].gameObject);
            _entries.RemoveAt(0);
        }
    }

    private void Authorize()
    {
        PlayerAccount.Authorize(onSuccessCallback: () =>
         {
             _logInButton.interactable = false;
             UpdateLeaderboard();
             Authorized?.Invoke();
         });
    }
}
