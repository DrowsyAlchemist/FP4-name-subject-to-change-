using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellRenderer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    [SerializeField] private Image _highlightedFrame;
    [SerializeField] private TMP_Text _levelText;

    public SpellData SpellData { get; private set; }

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

    public void Render(SpellData spellData)
    {
        if (SpellData != null)
            SpellData.Upgrated -= OnSpellUpgraded;

        SpellData = spellData ?? throw new ArgumentNullException();
        spellData.Upgrated += OnSpellUpgraded;
        UpdateRender(spellData);
    }

    private void UpdateRender(SpellData spellData)
    {
        _image.sprite = spellData.GetCurrentSpell().Sprite;
        _levelText.text = spellData.UpgradeLevel.ToString();
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke(this);
    }

    private void OnSpellUpgraded(SpellData spellData)
    {
        UpdateRender(spellData);
    }
}