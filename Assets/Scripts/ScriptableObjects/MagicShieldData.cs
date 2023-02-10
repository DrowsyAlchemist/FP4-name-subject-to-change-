using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicShield", menuName = "ScriptableObjects/MagicShield", order = 51)]
public class MagicShieldData : ScriptableObject
{
    [SerializeField] private Material _fireShieldMaterial;
    [SerializeField] private Material _windShieldMaterial;
    [SerializeField] private Material _lightningShieldMaterial;
    [SerializeField] private Material _earthShieldMaterial;
    [SerializeField] private Material _waterShieldMaterial;

    public Material GetMaterial(ElementType element)
    {
        return element switch
        {
            ElementType.Fire => _fireShieldMaterial,
            ElementType.Wind => _windShieldMaterial,
            ElementType.Lightning => _lightningShieldMaterial,
            ElementType.Earth => _earthShieldMaterial,
            ElementType.Water => _waterShieldMaterial,
            _ => throw new NotImplementedException(),
        };
    }
}