using System;

public class Health: ITakeDamage
{
    private readonly ElementType _element;

    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    public event Action<float> MaxHealthChanged;
    public event Action<float> CurrentHealthChanged;

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

        damage *= ElementsInteraction.GetInteractionModifier(transmittingElement, _element);
        CurrentHealth -= damage;

        if (CurrentHealth < 0)
            CurrentHealth = 0;

        CurrentHealthChanged?.Invoke(CurrentHealth);
    }

    public void IncreaseMaxHealth(int value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException("value");

        MaxHealth += value;
        MaxHealthChanged?.Invoke(MaxHealth);
    }

    public void RestoreHealth()
    {
        CurrentHealth = MaxHealth;
        CurrentHealthChanged?.Invoke(CurrentHealth);
    }
}