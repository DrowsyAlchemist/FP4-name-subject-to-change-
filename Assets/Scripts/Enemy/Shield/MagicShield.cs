using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class MagicShield : MonoBehaviour, ITakeDamage
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private HealthRenderer _healthRenderer;
    [SerializeField] private List<MeshRenderer> _meshRenderers = new();

    private Health _health;

    public bool IsDestroyed => _health.CurrentHealth <= 0;

    public event Action Destroyed;

    public void Init(ElementType element, Material material)
    {
        _health = new Health(_maxHealth, element);
        _healthRenderer.Render(_health);
        _health.HealthIsOver += OnHealthIsOver;

        foreach (var renderer in _meshRenderers)
            renderer.material = material;
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