using System;
using UnityEngine;
using UnityEngine.UI;

public class SpellRenderer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    [SerializeField] private Image _highlightedFrame;

    public UpgradeableSpellData SpellData { get; private set; }

    public event Action<SpellRenderer> ButtonClicked;

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void SetHighlighted(bool isHighlighted)
    {
        _highlightedFrame.gameObject.SetActive(isHighlighted);
    }

    public void Render(UpgradeableSpellData spellData)
    {
        if (SpellData != null)
            SpellData.Upgrated -= OnSpellUpgraded;

        SpellData = spellData ?? throw new ArgumentNullException();
        spellData.Upgrated += OnSpellUpgraded;
    }

    private void UpdateRender()
    {
        _image.sprite = SpellData.GetCurrentSpell().Sprite;
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke(this);
    }

    private void OnSpellUpgraded()
    {
        UpdateRender();
    }
}