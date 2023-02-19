using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class WallCollisionTransition : EnemyTransition
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Wall _))
            NeedTransit = true;
    }
}