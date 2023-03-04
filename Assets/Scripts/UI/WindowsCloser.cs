using UnityEngine;

public class WindowsCloser : MonoBehaviour
{
    [SerializeField] private RectTransform[] _windowsToClose;
    [SerializeField] private RectTransform _pauseWindow;

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
        {
            foreach (var window in _windowsToClose)
            {
                if (window.gameObject.activeSelf == true)
                {
                    CloseAllAndContinueGame();
                    return;
                }
            }
            Game.Pause();
        }

    }

    public static void CloseAllAndContinueGame()
    {
        foreach (var window in _instance._windowsToClose)
            window.gameObject.SetActive(false);

        if (_instance._pauseWindow.gameObject.activeSelf == false)
            Time.timeScale = 1;
    }
}