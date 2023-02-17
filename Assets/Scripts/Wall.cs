using System;
using UnityEngine;

public class Wall : MonoBehaviour, ITakeDamage
{
    [SerializeField] private float _initialhealth = 100;
    [SerializeField] private HealthRenderer _healthRenderer;
    [SerializeField] private ElementType _element;

    [SerializeField] private float _healthUpgradeModifier = 0.2f;
    [SerializeField] private float _healthRestoreModifier = 0.3f;

    private Health _health;

    public event Action Destroyed;

    private void Awake()
    {
        _health = new Health(_initialhealth, _element);
        _health.HealthIsOver += OnHealthOver;
        _healthRenderer.Render(_health);
    }

    public void TakeDamage(float damage, ElementType transmittingElement)
    {
        _health.TakeDamage(damage, transmittingElement);
    }

    public void Upgrade()
    {
        _health.IncreaseMaxHealth(_health.MaxHealth * _healthUpgradeModifier);
        _health.RestoreHealth(_health.MaxHealth * _healthRestoreModifier);
    }

    public void RestoreHealth(float percents)
    {
        if (percents < 0)
            throw new ArgumentOutOfRangeException();

        int maxPercent = 100;
        float restoreValue = _health.MaxHealth * percents / maxPercent;
        _health.RestoreHealth(restoreValue);
    }

    private void OnHealthOver()
    {
        _health.HealthIsOver -= OnHealthOver;
        Destroyed?.Invoke();
    }
}
