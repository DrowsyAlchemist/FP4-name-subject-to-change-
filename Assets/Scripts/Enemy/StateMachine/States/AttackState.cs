using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class AttackState : EnemyState
{
    [SerializeField] private int _damage = 5;
    [SerializeField] private float _secondsBetweenAttacks;
    [SerializeField] private float _attackDelay;

    private ElementType _element;
    private float _elapsedTime;

    private void Start()
    {
        _element = GetComponent<Enemy>().Element;
    }

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
        Game.Wall.TakeDamage(_damage, _element);
    }
}