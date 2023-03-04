using UnityEngine;

public class SecondLevelSpell : CombatSpell
{
    private int _hitCount;

    protected override void Hit(Collider collider)
    {
        if (collider.TryGetComponent(out ITakeDamage subject))
        {
            _hitCount++;
            subject.TakeDamage(Damage/_hitCount, Element);

            if (collider.TryGetComponent(out MagicShield _))
                Destroy(gameObject);
        }
    }
}