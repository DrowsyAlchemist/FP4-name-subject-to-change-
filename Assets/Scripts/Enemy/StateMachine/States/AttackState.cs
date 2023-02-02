using System.Collections;
using UnityEngine;

public class AttackState : EnemyState
{
    [SerializeField] private int _damage = 5;
    [SerializeField] private float _secondsBetweenAttacks;
    [SerializeField] private float _attackDelay;

    private float _elapsedTime;

    private void OnEnable()
    {
        _elapsedTime = _secondsBetweenAttacks;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _secondsBetweenAttacks)
        {
            _elapsedTime = 0;
            StartCoroutine(AttackWall());
        }

    }

    private IEnumerator AttackWall()
    {
        EnemyAnimator.PlayAttack();
        yield return new WaitForSeconds(_attackDelay);
        Game.Wall.TakeDamage(_damage);
    }
}