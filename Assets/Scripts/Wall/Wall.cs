using System;
using UnityEngine;

public class Wall : MonoBehaviour, ITakeDamage
{
    [SerializeField] private float _initialhealth = 10;
    [SerializeField] private HealthRenderer _healthRenderer;
    [SerializeField] private ElementType _element;

    public Health Health { get; private set; }

    public event Action Destroyed;

    private void Awake()
    {
        Health = new Health(_initialhealth, _element);
        Health.HealthIsOver += OnHealthOver;
        _healthRenderer.Render(Health);
    }

    public void TakeDamage(float damage, ElementType transmittingElement)
    {
        Health.TakeDamage(damage, transmittingElement);
    }

    private void OnHealthOver()
    {
        Health.HealthIsOver -= OnHealthOver;
        Destroyed?.Invoke();
    }
}
