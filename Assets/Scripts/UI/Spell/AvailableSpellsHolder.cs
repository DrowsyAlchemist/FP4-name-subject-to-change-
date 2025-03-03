using System.Collections.Generic;
using UnityEngine;

public class AvailableSpellsHolder : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private SpellRenderer _spellRenderer;
    [SerializeField] private SpellCaster _caster;

    private readonly List<SpellRenderer> _spells = new();
    private SpellRenderer _highlighted;

    private void OnDestroy()
    {
        foreach (var renderer in _container.GetComponentsInChildren<SpellRenderer>())
            renderer.ButtonClicked -= OnRendererClick;
    }

    public void SetDefaultSpell(SpellData defaultSpell)
    {
        while (_spells.Count > 0)
        {
            Destroy(_spells[0].gameObject);
            _spells.RemoveAt(0);
        }
        var defaultSpellRenderer = AddSpell(defaultSpell);
        OnRendererClick(defaultSpellRenderer);
    }

    public SpellRenderer AddSpell(SpellData spellData)
    {
        var spellRenderer = Instantiate(_spellRenderer, _container);
        spellRenderer.Render(spellData);
        spellRenderer.ButtonClicked += OnRendererClick;
        _spells.Add(spellRenderer);
        return spellRenderer;
    }

    private void OnRendererClick(SpellRenderer spellRenderer)
    {
        if (_highlighted != null)
            _highlighted.SetHighlighted(false);

        spellRenderer.SetHighlighted(true);
        _highlighted = spellRenderer;
        _caster.SetSpell(spellRenderer.SpellData.Element);
    }
}