using UnityEngine;

public class FirstLevelCombatSpell : CombatSpell
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ITakeDamage subject))
            subject.TakeDamage(Damage, Element);

        Collapse();
    }
    private void Collapse()
    {
        Destroy(gameObject);
    }
}
