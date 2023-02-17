using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class MagicShield : MonoBehaviour, ITakeDamage
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _speed;
    [SerializeField] private HealthRenderer _healthRenderer;
    [SerializeField] private MagicShieldData _data;
    [SerializeField] private MeshRenderer _meshRenderer;

    private Rigidbody _rigidbody;
    private Health _health;

    public float Speed => _speed;

    public event Action Destroyed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        ElementType element = (ElementType)UnityEngine.Random.Range(1, 6);
        _meshRenderer.material = _data.GetMaterial(element);
        _health = new Health(_maxHealth, element);
        _healthRenderer.Render(_health);
        _health.HealthIsOver += OnHealthIsOver;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, -1 * _speed);
    }

    public void TakeDamage(float damage, ElementType element)
    {
        _health.TakeDamage(damage, element);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Wall _))
            Collapse();
    }

    private void OnHealthIsOver()
    {
        _health.HealthIsOver -= OnHealthIsOver;
        Collapse();
    }

    private void Collapse()
    {
        Destroyed?.Invoke();
        Destroy(gameObject);
    }
}