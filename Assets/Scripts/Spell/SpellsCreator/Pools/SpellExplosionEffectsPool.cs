using UnityEngine;

public class SpellExplosionEffectsPool : Pool
{
    [SerializeField] private SpellExplosionEffect _template;

    private void Awake()
    {
        base.Init(_template.gameObject);
    }

    public SpellExplosionEffect GetEffect()
    {
        return base.GetObject().GetComponent<SpellExplosionEffect>();
    }
}