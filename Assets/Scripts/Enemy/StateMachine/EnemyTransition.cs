using UnityEngine;

public abstract class EnemyTransition : MonoBehaviour
{
    [SerializeField] private EnemyState _targetState;

    public EnemyState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    private void Awake()
    {
        enabled = false;
    }

    protected virtual void OnEnable()
    {
        NeedTransit = false;
    }
}