using UnityEngine;

public abstract class UpgradeableSpell : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _lable;
    [SerializeField] private int _cost;

    public Sprite Sprite => _sprite;
    public string Lable => _lable;
    public int Cost => _cost;
}