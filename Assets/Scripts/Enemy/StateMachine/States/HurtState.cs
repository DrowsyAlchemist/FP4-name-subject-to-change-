using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HurtState : EnemyState
{
    private Rigidbody _rigidbody;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        EnemyAnimator.PlayHurt();
    }

    private void Update()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}