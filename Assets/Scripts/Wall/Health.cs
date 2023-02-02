using System;

public class Health
{
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    public event Action<int> MaxHealthChanged;
    public event Action<int> CurrentHealthChanged;

    public Health(int maxHealth)
    {
        if (maxHealth <= 0)
            throw new ArgumentOutOfRangeException();

        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void ReduceHealth(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException();

        CurrentHealth -= value;

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