using UnityEngine;

public class AvailableSpellsHolder : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private SpellRenderer _spellRenderer;
    [SerializeField] private SpellCaster _caster;

    private SpellRenderer _highlighted;

    private void OnDestroy()
    {
        foreach (var renderer in _container.GetComponentsInChildren<SpellRenderer>())
            renderer.ButtonClicked -= OnRendererClick;
    }

    public void AddSpell(UpgradeableSpellData spellData)
    {
        var spellRenderer = Instantiate(_spellRenderer, _container);
        spellRenderer.Render(spellData);
        spellRenderer.ButtonClicked += OnRendererClick;
    }

    private void OnRendererClick(SpellRenderer spellRenderer)
    {
        if (spellRenderer.SpellData.GetCurrentSpell() is CombatSpell combatSpell)
        {
            if (_highlighted != null)
                _highlighted.SetHighlighted(false);

            spellRenderer.SetHighlighted(true);
            _highlighted = spellRenderer;
            _caster.SetSpell(combatSpell);
        }
    }
}