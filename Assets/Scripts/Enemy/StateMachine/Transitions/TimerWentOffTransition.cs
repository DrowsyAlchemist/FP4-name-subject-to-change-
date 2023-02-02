using UnityEngine;

public class TimerWentOffTransition : EnemyTransition
{
    [SerializeField] private float _seconds;

    private float _elapsedTime;

    protected override void OnEnable()
    {
        base.OnEnable();
        _elapsedTime = 0;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _seconds)
            NeedTransit = true;
    }
}