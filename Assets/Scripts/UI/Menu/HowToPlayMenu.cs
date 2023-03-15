using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayMenu : Menu
{
    [SerializeField] private List<RectTransform> _pages;
    [SerializeField] private Button _nextPageButton;
    [SerializeField] private Button _previousPageButton;
    [SerializeField] private TMP_Text _pageText;

    private int _currentPage;

    private void OnEnable()
    {
        foreach (var page in _pages)
            page.gameObject.SetActive(false);

        ShowPage(0);
        _nextPageButton.gameObject.SetActive(true);
    }

    private void Start()
    {
        _nextPageButton.onClick.AddListener(OnNextPageButtonClick);
        _previousPageButton.onClick.AddListener(OnPreviousPageButtonClick);
    }

    private void OnDestroy()
    {
        _nextPageButton.onClick.RemoveListener(OnNextPageButtonClick);
        _previousPageButton.onClick.RemoveListener(OnPreviousPageButtonClick);
    }

    private void OnNextPageButtonClick()
    {
        ShowPage(_currentPage + 1);
    }

    private void OnPreviousPageButtonClick()
    {
        ShowPage(_currentPage - 1);
    }

    private void ShowPage(int page)
    {
        _pages[_currentPage].gameObject.SetActive(false);
        _currentPage = page;
        _pages[page].gameObject.SetActive(true);
        _pageText.text = (page + 1) + "/" + _pages.Count;
        _previousPageButton.gameObject.SetActive(_currentPage != 0);
        _nextPageButton.gameObject.SetActive(_currentPage != _pages.Count - 1);
    }
}
