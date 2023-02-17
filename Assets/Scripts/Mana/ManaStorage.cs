using System;

public class ManaStorage
{
    public int Amount { get; private set; } = 1000;

    public event Action<int> AmountChanged;

    public void TakeMana(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException();

        Amount += amount;
        AmountChanged?.Invoke(Amount);
    }

    public bool CanGiveMana(int amount)
    {
        return Amount >= amount;
    }

    public int GiveMana(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException();

        if (CanGiveMana(amount) == false)
            throw new InvalidOperationException();

        Amount -= amount;
        AmountChanged?.Invoke(Amount);
        return amount;
    }
}