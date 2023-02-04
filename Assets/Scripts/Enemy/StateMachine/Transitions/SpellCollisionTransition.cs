using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class SpellCollisionTransition : EnemyTransition
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CombatSpell _))
            NeedTransit = true;
    }
}