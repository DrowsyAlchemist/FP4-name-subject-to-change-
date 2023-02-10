using UnityEngine;

public class ThirdLevelSpell : CombatSpell
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out MagicShield shield))
        {
            shield.TakeDamage(Damage, Element);
            Collapse();
        }
        if (other.TryGetComponent(out ITakeDamage subject))
            subject.TakeDamage(Damage, Element);
    }

    private void Collapse()
    {
        Destroy(gameObject);
    }
}