using UnityEngine;

public class FirstLevelCombatSpell : CombatSpell
{
    protected override void Hit(Collider collider)
    {
        if (collider.TryGetComponent(out ITakeDamage subject))
            subject.TakeDamage(Damage, Element);

        Collapse();
    }

    private void Collapse()
    {
        Destroy(gameObject);
    }
}
