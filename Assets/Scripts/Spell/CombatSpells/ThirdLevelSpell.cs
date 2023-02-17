using UnityEngine;

public class ThirdLevelSpell : CombatSpell
{
    protected override void Hit(Collider collider)
    {
        if (collider.TryGetComponent(out MagicShield shield))
        {
            shield.TakeDamage(Damage, Element);
            Collapse();
        }
        if (collider.TryGetComponent(out ITakeDamage subject))
            subject.TakeDamage(Damage, Element);
    }

    private void Collapse()
    {
        Destroy(gameObject);
    }
}