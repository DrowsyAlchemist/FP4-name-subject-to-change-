using System;
using UnityEngine;

public class Wall : MonoBehaviour, ITakeDamage
{
    [SerializeField] private float _initialhealth = 100;
    [SerializeField] private float _wallRepairBetweenLevelsPercents = 15;
    [SerializeField] private ElementType _element;
    [SerializeField] private HealthRenderer _healthRenderer;
    [SerializeField] private Game _game;

    private Health _health;

    public IReadonlyHealth Health => _health;

    public event Action Destroyed;

    private void Awake()
    {
        _health = new Health(_initialhealth, _element);
        _health.HealthIsOver += OnHealthOver;
        _healthRenderer.Render(_health);
        _game.LevelFinished += () => Repair(_wallRepairBetweenLevelsPercents);
    }

    public void TakeDamage(float damage, ElementType transmittingElement)
    {
        _health.TakeDamage(damage, transmittingElement);
    }

    public void IncreaseMaxHealth(float value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException();

        _health.IncreaseMaxHealth(value);
        _health.RestoreHealth(value);
    }

    public void Repair(float percents)
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
