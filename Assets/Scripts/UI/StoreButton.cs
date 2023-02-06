using UnityEngine;
using UnityEngine.UI;

public class StoreButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private RectTransform _storeMenu;

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        bool isMenuOpen = _storeMenu.gameObject.activeSelf;
        Time.timeScale = isMenuOpen ? 1 : 0;
        _storeMenu.gameObject.SetActive(isMenuOpen == false);
    }
}