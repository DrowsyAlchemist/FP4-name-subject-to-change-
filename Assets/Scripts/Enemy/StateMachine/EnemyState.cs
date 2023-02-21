using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(Enemy))]
public abstract class EnemyState : MonoBehaviour
{
    [SerializeField] private EnemyTransition[] _transitions;

    protected Enemy Enemy { get; private set; }
    protected CharacterAnimator EnemyAnimator { get; private set; }

    protected virtual void Awake()
    {
        Enemy = GetComponent<Enemy>();
        EnemyAnimator = GetComponent<CharacterAnimator>();
        enabled = false;
    }

    public void Enter()
    {
        enabled = true;

        foreach (var transition in _transitions)
            transition.enabled = true;
    }

    public void Exit()
    {
        foreach (var transition in _transitions)
            transition.enabled = false;

        enabled = false;
    }

    public EnemyState GetNextState()
    {
        foreach (var transition in _transitions)
            if (transition.NeedTransit)
                return transition.TargetState;

        return null;
    }
}