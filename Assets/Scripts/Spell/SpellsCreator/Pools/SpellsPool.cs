using UnityEngine;

public class SpellsPool : Pool
{
    [SerializeField] private Spell _template;

    private void Awake()
    {
        base.Init(_template.gameObject);
    }

    public Spell GetSpell()
    {
        return base.GetObject().GetComponent<Spell>();
    }
}