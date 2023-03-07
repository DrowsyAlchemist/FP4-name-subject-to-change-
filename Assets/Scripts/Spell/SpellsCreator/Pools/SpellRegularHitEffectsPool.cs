using UnityEngine;

public class SpellRegularHitEffectsPool : Pool
{
    [SerializeField] private SpellRegularHitEffect _template;

    private void Awake()
    {
        base.Init(_template.gameObject);
    }

    public SpellRegularHitEffect GetEffect()
    {
        return base.GetObject().GetComponent<SpellRegularHitEffect>();
    }
}