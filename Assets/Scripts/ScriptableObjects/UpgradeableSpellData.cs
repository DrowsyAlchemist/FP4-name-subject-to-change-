using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeableItem", menuName = "ScriptableObjects/UpgradeableItem", order = 51)]
public class UpgradeableSpellData : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private int _maxLevel = 4;

    [SerializeField] private UpgradeableSpell _firstLevelSpell;
    [SerializeField] private UpgradeableSpell _secondLevelSpell;
    [SerializeField] private UpgradeableSpell _thirdLevelSpell;
    [SerializeField] private UpgradeableSpell _fourthLevelSpell;

    public int MaxLevel => _maxLevel;
    public int UpgradeLevel => PlayerPrefs.GetInt(_id, 0);

    public event Action<UpgradeableSpellData> Upgrated;

    public UpgradeableSpell GetCurrentSpell()
    {
        return GetSpell(UpgradeLevel);
    }

    public UpgradeableSpell GetSpell(int upgradeLevel)
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