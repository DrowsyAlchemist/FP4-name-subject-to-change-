using UnityEngine;
using System;
using Lean.Localization;

[CreateAssetMenu(fileName = "SpellResources", menuName = "ScriptableObjects/SpellResources", order = 51)]
public class SpellData : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private int _maxLevel = 4;

    [SerializeField] private SpellUpgrageLevelInfo _upgrageLevelInfo;
    [SerializeField] private ElementType _element;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Material _material;
    [SerializeField] private Color _effectColor;
    [SerializeField] private string _lable;

    public ElementType Element => _element;
    public Sprite Sprite => _sprite;
    public Material Material => _material;
    public Color EffectColor => _effectColor;
    public string Lable => GetLable();
    public int MaxLevel => _maxLevel;
    public int UpgradeLevel => PlayerPrefs.GetInt(_id, 0);

    public event Action<SpellData> Upgrated;

    public float GetDamage()
    {
        return _upgrageLevelInfo.GetDamage(UpgradeLevel);
    }

    public int GetNextLevelCost()
    {
        return _upgrageLevelInfo.GetCost(UpgradeLevel + 1);
    }

    public void ResetSpell()
    {
        PlayerPrefs.SetInt(_id, 0);
        PlayerPrefs.Save();
    }

    public void Upgrade()
    {
        PlayerPrefs.SetInt(_id, UpgradeLevel + 1);
        PlayerPrefs.Save();
        Upgrated?.Invoke(this);
    }

    private string GetLable()
    {
        return LeanLocalization.GetTranslationText(_lable);
    }
}
