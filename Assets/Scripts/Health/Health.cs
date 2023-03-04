using System;

public class Health: ITakeDamage, IReadonlyHealth
{
    private readonly ElementType _element;

    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    public event Action<float> MaxHealthChanged;
    public event Action<float> CurrentHealthChanged;
    public event Action HealthIsOver;

    public Health(float maxHealth, ElementType element)
    {
        if (maxHealth <= 0)
            throw new ArgumentOutOfRangeException();

        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        _element = element;
    }

    public void TakeDamage(float damage, ElementType transmittingElement)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException();

        if (CurrentHealth <= 0)
            return;

        damage *= Element.GetInteractionModifier(transmittingElement, _element);
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            HealthIsOver?.Invoke();
        }
        CurrentHealthChanged?.Invoke(CurrentHealth);
    }

    public void IncreaseMaxHealth(float value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException("value");

        MaxHealth += value;
        MaxHealthChanged?.Invoke(MaxHealth);
    }

    public void RestoreHealth(float value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException("value");

        CurrentHealth += value;

        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;

        CurrentHealthChanged?.Invoke(CurrentHealth);
    }

    public void Reset(float maxHealth)
    {
        if (maxHealth <= 0)
            throw new ArgumentOutOfRangeException();

        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        CurrentHealthChanged?.Invoke(CurrentHealth);
        MaxHealthChanged?.Invoke(MaxHealth);
    }
}