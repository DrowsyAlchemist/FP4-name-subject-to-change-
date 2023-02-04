using System;
using UnityEngine;
using UnityEngine.UI;

public class SpellRenderer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    private UpgradeableSpell _spell;

    public event Action<UpgradeableSpell> ButtonClicked;

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(UpgradeableSpell spell)
    {
        _spell = spell??throw new ArgumentNullException();
        _image.sprite = spell.Data.Sprite;
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke(_spell);
    }
}