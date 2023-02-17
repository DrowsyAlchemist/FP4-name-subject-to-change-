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
        {
            renderer.ButtonClicked -= OnRendererClick;
            renderer.SpellData.Upgrated -= OnSpellUpgraded;
        }
    }

    public void SetDefaultSpell(UpgradeableSpellData defaultSpell)
    {
        var defaultSpellRenderer = AddSpell(defaultSpell);
        OnRendererClick(defaultSpellRenderer);
    }

    public SpellRenderer AddSpell(UpgradeableSpellData spellData)
    {
        spellData.Upgrated += OnSpellUpgraded;
        var spellRenderer = Instantiate(_spellRenderer, _container);
        spellRenderer.Render(spellData);
        spellRenderer.ButtonClicked += OnRendererClick;
        return spellRenderer;
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
        else if (spellRenderer.SpellData.GetCurrentSpell() is WallRepairSpell wallRepairSpell)
        {
            wallRepairSpell.Use();
        }
        else
        {
            throw new System.NotImplementedException();
        }
    }

    private void OnSpellUpgraded(UpgradeableSpellData spellData)
    {
        if (spellData == _highlighted.SpellData)
            _caster.SetSpell(spellData.GetCurrentSpell() as CombatSpell);
    }
}