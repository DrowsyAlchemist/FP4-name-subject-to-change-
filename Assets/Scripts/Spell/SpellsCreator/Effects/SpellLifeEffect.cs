using System.Collections.Generic;
using UnityEngine;

public class SpellLifeEffect : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _particles;

    public void SetColor(Color color)
    {
        foreach (var particle in _particles)
            particle.startColor = color;
    }
}