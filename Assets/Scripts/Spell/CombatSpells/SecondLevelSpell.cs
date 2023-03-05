using UnityEngine;

public class SecondLevelSpell : CombatSpell
{
    private const float _scaleReduseModifier = 0.75f;
    private int _hitCount;

    protected override void Hit(Collider collider)
    {
        if (collider.TryGetComponent(out ITakeDamage subject))
        {
            _hitCount++;
            subject.TakeDamage(Damage/_hitCount, Element);
            transform.localScale *= _scaleReduseModifier;

            if (collider.TryGetComponent(out MagicShield _))
                Destroy(gameObject);
        }
    }
}