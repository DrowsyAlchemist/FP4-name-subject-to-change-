using UnityEngine;

public class SpellsCreator : MonoBehaviour
{
    [SerializeField] private SpellsPool _spellsPool;
    [SerializeField] private SpellRegularHitEffectsPool _hitEffectsPool;
    [SerializeField] private SpellExplosionEffectsPool _explosionEffectsPool;

    [SerializeField] private SpellData _lightSpellResources;
    [SerializeField] private SpellData _fireSpellResources;
    [SerializeField] private SpellData _windSpellResources;
    [SerializeField] private SpellData _lightningSpellResources;
    [SerializeField] private SpellData _earthSpellResources;
    [SerializeField] private SpellData _waterSpellResources;

    public Spell Create(ElementType element)
    {
        var spellResources = GetResources(element);
        int upgradeLevel = spellResources.UpgradeLevel;
        SpellHitEffect hitEffect = (upgradeLevel > 2) ? _explosionEffectsPool.GetEffect() : _hitEffectsPool.GetEffect();
        hitEffect.gameObject.SetActive(true);

        var spell = _spellsPool.GetSpell();
        spell.ResetSpell(spellResources, hitEffect);
        spell.gameObject.SetActive(true);
        return spell;
    }

    private SpellData GetResources(ElementType element)
    {
        return element switch
        {
            ElementType.Light => _lightSpellResources,
            ElementType.Fire => _fireSpellResources,
            ElementType.Wind => _windSpellResources,
            ElementType.Lightning => _lightningSpellResources,
            ElementType.Earth => _earthSpellResources,
            ElementType.Water => _waterSpellResources,
            _ => throw new System.NotImplementedException(),
        };
    }
}