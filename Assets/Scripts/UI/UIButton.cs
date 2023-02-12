using UnityEngine;
using UnityEngine.UI;

public abstract class UIButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    protected abstract void OnButtonClick();
}
