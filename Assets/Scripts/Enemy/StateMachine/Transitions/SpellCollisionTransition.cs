using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class SpellCollisionTransition : EnemyTransition
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Spell _))
            NeedTransit = true;
    }
}