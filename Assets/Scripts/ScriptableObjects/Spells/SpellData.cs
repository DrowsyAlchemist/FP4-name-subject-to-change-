using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "ScriptableObjects/Spell", order = 51)]
public class SpellData : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _lable;
    [SerializeField] private int _cost;

    public Sprite Sptite => _sprite;
    public string Lable => _lable;
    public int Cost => _cost;
}
