using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeableItem", menuName = "ScriptableObjects/UpgradeableItem", order = 51)]
public class UpgradeableSpellData : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _lable;
    [SerializeField] private int _cost;
    [SerializeField] private int _secondLevelUpgradeCost;
    [SerializeField] private int _thirdLevelUpgradeCost;
    [SerializeField] private int _fourthLevelUpgradeCost;

    public Sprite Sprite => _sprite;
    public string Lable => _lable;
    public int Cost => _cost;
    public int SecondLevelUpgradeCost { get; }
    public int ThirdLevelUpgradeCost { get; }
    public int FourthLevelUpgradeCost { get; }
}
