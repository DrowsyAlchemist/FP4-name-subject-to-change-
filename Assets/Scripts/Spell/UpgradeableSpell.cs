using Lean.Localization;
using UnityEngine;

public abstract class UpgradeableSpell : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _lable;
    [SerializeField] private int _cost;

    public Sprite Sprite => _sprite;
    public string Lable => GetLable();
    public int Cost => _cost;

    private string GetLable()
    {
       return LeanLocalization.GetTranslationText(_lable);
    }
}