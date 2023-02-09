using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveForwardState : EnemyState
{
    [SerializeField] private float _speed = 2;

    private Rigidbody _rigidbody;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        EnemyAnimator.PlayWalk();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, -1 * _speed);
    }
}