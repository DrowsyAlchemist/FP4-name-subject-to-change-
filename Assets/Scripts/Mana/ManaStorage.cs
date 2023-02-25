using System;

public class ManaStorage
{
    private int _savedAmount;

    public int Amount { get; private set; } // = 100000;

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

    public void Reset()
    {
        Amount = 0;
        _savedAmount = 0;
        AmountChanged?.Invoke(Amount);
    }

    public void Save()
    {
        _savedAmount = Amount;
    }

    public void Load()
    {
        Amount = _savedAmount;
        AmountChanged?.Invoke(Amount);
    }
}