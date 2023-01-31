using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "ScriptableObjects/Spell", order = 51)]
public class SpellData : ScriptableObject
{
    [SerializeField] private string _lable;
    [SerializeField] private int _cost;
}
