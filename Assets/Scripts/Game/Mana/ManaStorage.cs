using System;

public class ManaStorage
{
    private int _savedAmount;

    public int Amount { get; private set; }

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

    public void Reset(int initialAmount = 0)
    {
        Amount = initialAmount;
        _savedAmount = Amount;
        AmountChanged?.Invoke(Amount);
    }

    public void Save()
    {
        _savedAmount = Amount;
    }

    public void Load()
    {
        if (Amount > _savedAmount)
            Amount = _savedAmount;

        AmountChanged?.Invoke(Amount);
    }
}