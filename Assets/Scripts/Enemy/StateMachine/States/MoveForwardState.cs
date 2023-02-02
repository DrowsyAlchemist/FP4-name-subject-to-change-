using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveForwardState : EnemyState
{
    [SerializeField] private float _speed = 2;

    private readonly Vector3 _defaultDirection = Vector3.back;
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
        _rigidbody.velocity = _speed * _defaultDirection;
    }
}