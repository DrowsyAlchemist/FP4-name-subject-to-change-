using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Enemy : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int _initialHealth = 3;

    private Mover _mover;
    private Health _health;

    public event Action Died;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _health = new Health(_initialHealth);
        _health.CurrentHealthChanged += OnHealthChanged;
    }

    private void OnDestroy()
    {
        _health.CurrentHealthChanged -= OnHealthChanged;
    }

    private void Start()
    {
        _mover.StartMovement();
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException();

        _health.ReduceHealth(damage);
    }

    private void OnHealthChanged(int health)
    {
        if (health <= 0)
        {
            Die();
            Died?.Invoke();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}