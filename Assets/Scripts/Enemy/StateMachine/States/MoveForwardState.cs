using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveForwardState : EnemyState
{
    [SerializeField] private float _speed = 2;
    [SerializeField] private float _speedWithShield = 1;

    private Rigidbody _rigidbody;
    private float _currentSpeed;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (Enemy.HasShield)
            Enemy.Shield.Destroyed += OnShieldDestroyed;
    }

    private void OnEnable()
    {
        EnemyAnimator.PlayWalk();

        if (Enemy.HasShield && Enemy.Shield.IsDestroyed == false)
            _currentSpeed = _speedWithShield;
        else
            _currentSpeed = _speed;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, -1 * _currentSpeed);
    }

    private void OnShieldDestroyed()
    {
        Enemy.Shield.Destroyed -= OnShieldDestroyed;
        _currentSpeed = _speed;
    }
}