using System;
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

    private void OnHealthChanged(float _)
    {
        UpdateRenderer();
    }

    private void UpdateRenderer()
    {
        if (_healthText != null)
        {
            int currentHealth = (int)Math.Round(_health.CurrentHealth, MidpointRounding.AwayFromZero);
            int maxHealth = (int)Math.Round(_health.MaxHealth, MidpointRounding.AwayFromZero);
            _healthText.text = currentHealth + " / " + maxHealth;
        }
        _slider.value = _health.CurrentHealth / _health.MaxHealth;
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