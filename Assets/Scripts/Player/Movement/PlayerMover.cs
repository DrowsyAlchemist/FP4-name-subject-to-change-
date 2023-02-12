using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : Mover
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _speed = 8;

    private Rigidbody _rigidbody;

    protected override void Awake()
    {
        base.Awake();

        if (_input == null)
            throw new System.NullReferenceException(nameof(_input));

        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 direction = _input.GetDirection().normalized;
        _rigidbody.velocity = direction * _speed;
    }
}