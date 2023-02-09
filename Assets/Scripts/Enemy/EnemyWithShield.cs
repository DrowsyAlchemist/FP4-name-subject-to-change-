using UnityEngine;

public class EnemyWithShield : Enemy
{
    [SerializeField] private MagicShield _shieldTemplate;
    [SerializeField] private Transform _shieldPoint;

    public MagicShield Shield { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Shield = Instantiate(_shieldTemplate, _shieldPoint.position, Quaternion.identity, null);
    }
}