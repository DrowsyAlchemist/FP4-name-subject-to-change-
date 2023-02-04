using UnityEngine;

public class AvailableSpellsHolder : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private SpellRenderer _spellRenderer;
    [SerializeField] private SpellCaster _caster;

    public void AddSpell(UpgradeableSpell spell)
    {
        var spellRenderer = Instantiate(_spellRenderer, _container);
        spellRenderer.Render(spell);
        spellRenderer.ButtonClicked += OnRendererClick;
    }

    private void OnRendererClick(UpgradeableSpell spell)
    {
        if (spell is CombatSpell combatSpell)
            _caster.SetSpell(combatSpell);
    }
}