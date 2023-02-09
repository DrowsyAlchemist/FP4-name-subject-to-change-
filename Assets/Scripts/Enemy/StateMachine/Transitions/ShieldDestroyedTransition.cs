using UnityEngine;

[RequireComponent(typeof(EnemyWithShield))]
public class ShieldDestroyedTransition : EnemyTransition
{
    private MagicShield _shield;

    protected override void OnEnable()
    {
        base.OnEnable();
        _shield = GetComponent<EnemyWithShield>().Shield;
        _shield.Destroyed += () => NeedTransit = true;
    }
}