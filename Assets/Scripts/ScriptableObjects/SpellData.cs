using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "ScriptableObjects/Spell", order = 51)]
public class SpellData : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private int _maxLevel = 4;

    [SerializeField] private Spell _firstLevelSpell;
    [SerializeField] private Spell _secondLevelSpell;
    [SerializeField] private Spell _thirdLevelSpell;
    [SerializeField] private Spell _fourthLevelSpell;

    public int MaxLevel => _maxLevel;
    public int UpgradeLevel => PlayerPrefs.GetInt(_id, 0);

    public event Action<SpellData> Upgrated;

    public Spell GetCurrentSpell()
    {
        return GetSpell(UpgradeLevel);
    }

    public Spell GetSpell(int upgradeLevel)
    {
        switch (upgradeLevel)
        {
            case 1:
                return _firstLevelSpell;
            case 2:
                return _secondLevelSpell;
            case 3:
                return _thirdLevelSpell;
            case 4:
                return _fourthLevelSpell;
            default:
                throw new NotImplementedException();
        }
    }

    public int GetNextLevelCost()
    {
        return GetSpell(UpgradeLevel + 1).Cost;
    }

    public void Upgrade()
    {
        PlayerPrefs.SetInt(_id, UpgradeLevel + 1);
        PlayerPrefs.Save();
        Upgrated?.Invoke(this);
    }
}