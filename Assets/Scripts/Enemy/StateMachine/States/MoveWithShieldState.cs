using UnityEngine;

[RequireComponent(typeof(EnemyWithShield))]
[RequireComponent(typeof(Rigidbody))]
public class MoveWithShieldState : EnemyState
{
    private float _shieldSpeed;
    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        _shieldSpeed = GetComponent<EnemyWithShield>().Shield.Speed;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, -1 * _shieldSpeed);
    }
}