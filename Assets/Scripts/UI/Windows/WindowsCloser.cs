using UnityEngine;

public class WindowsCloser : MonoBehaviour
{
    [SerializeField] private RectTransform[] _windowsToClose;

    private static WindowsCloser _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(_instance);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseAllAndContinueGame();
    }

    public static void CloseAllAndContinueGame()
    {
        foreach (var window in _instance._windowsToClose)
            window.gameObject.SetActive(false);

        Time.timeScale = 1;
    }
}