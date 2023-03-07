using UnityEngine;

[CreateAssetMenu(fileName = "SpellUpgrageLevelInfo", menuName = "ScriptableObjects/SpellUpgrageLevelInfo", order = 51)]
public class SpellUpgrageLevelInfo : ScriptableObject
{
    [SerializeField] private int _firstLevelCost;
    [SerializeField] private int _secondLevelCost;
    [SerializeField] private int _thirdLevelCost;
    [SerializeField] private int _fourthLevelCost;

    [SerializeField] private float _firstLevelDamage;
    [SerializeField] private float _secondLevelDamage;
    [SerializeField] private float _thirdLevelDamage;
    [SerializeField] private float _fourthLevelDamage;

    public int GetCost(int upgradeLevel)
    {
        return upgradeLevel switch
        {
            1 => _firstLevelCost,
            2 => _secondLevelCost,
            3 => _thirdLevelCost,
            4 => _fourthLevelCost,
            _ => throw new System.ArgumentOutOfRangeException(),
        };
    }

    public float GetDamage(int upgradeLevel)
    {
        return upgradeLevel switch
        {
            1 => _firstLevelDamage,
            2 => _secondLevelDamage,
            3 => _thirdLevelDamage,
            4 => _fourthLevelDamage,
            _ => throw new System.ArgumentOutOfRangeException(),
        };
    }
}