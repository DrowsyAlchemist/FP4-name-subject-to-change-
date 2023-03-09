using UnityEngine;
using UnityEngine.UI;

public abstract class UIButton : MonoBehaviour
{
    [SerializeField] protected Button Button;

    protected virtual void Start()
    {
        Button.onClick.AddListener(OnButtonClick);
    }

    protected virtual void OnDestroy()
    {
        Button.onClick.RemoveListener(OnButtonClick);
    }

    protected abstract void OnButtonClick();
}
