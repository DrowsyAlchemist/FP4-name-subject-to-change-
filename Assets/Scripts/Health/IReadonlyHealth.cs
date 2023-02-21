using System;

public interface IReadonlyHealth
{
    public float MaxHealth { get; }
    public float CurrentHealth { get; }

    public event Action<float> MaxHealthChanged;
    public event Action<float> CurrentHealthChanged;
    public event Action HealthIsOver;
}