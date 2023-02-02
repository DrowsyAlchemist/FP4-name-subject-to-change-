using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthRenderer : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _healthText;

    private Health _health;

    private void OnDestroy()
    {
        UnsubscribeFromHealth(_health);
    }

    public void Render(Health health)
    {
        UnsubscribeFromHealth(_health);
        _health = health ?? throw new System.ArgumentNullException();
        UpdateRenderer();
        health.MaxHealthChanged += OnHealthChanged;
        health.CurrentHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int _)
    {
        UpdateRenderer();
    }

    private void UpdateRenderer()
    {
        if (_healthText != null)
            _healthText.text = _health.CurrentHealth + " / " + _health.MaxHealth;

        _slider.value = (float)_health.CurrentHealth / _health.MaxHealth;
    }

    private void UnsubscribeFromHealth(Health health)
    {
        if (health != null)
        {
            health.MaxHealthChanged -= OnHealthChanged;
            health.CurrentHealthChanged -= OnHealthChanged;
        }
    }
}