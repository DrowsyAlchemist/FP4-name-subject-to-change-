using UnityEngine;

public class Wall : MonoBehaviour, ITakeDamage
{
    [SerializeField] private float _initialhealth = 10;
    [SerializeField] private HealthRenderer _healthRenderer;
    [SerializeField] private ElementType _element;

    public Health Health { get; private set; }

    private void Awake()
    {
        Health = new Health(_initialhealth, _element);
        _healthRenderer.Render(Health);
    }

    public void TakeDamage(float damage, ElementType transmittingElement)
    {
        Health.TakeDamage(damage, transmittingElement);
    }
}
