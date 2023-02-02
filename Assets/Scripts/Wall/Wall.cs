using UnityEngine;

public class Wall : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int _initialhealth = 10;
    [SerializeField] private HealthRenderer _healthRenderer;

    public Health Health { get; private set; }

    private void Awake()
    {
        Health = new Health(_initialhealth);
        _healthRenderer.Render(Health);
    }

    public void TakeDamage(int damage)
    {
        Health.ReduceHealth(damage);
    }
}
