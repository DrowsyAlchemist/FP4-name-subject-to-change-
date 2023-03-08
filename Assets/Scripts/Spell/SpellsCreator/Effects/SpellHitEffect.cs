using System.Collections.Generic;
using UnityEngine;

public abstract class SpellHitEffect : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _effects;

    private void FixedUpdate()
    {
        if (transform.parent == null)
            if (_effects[0].IsAlive() == false)
                gameObject.SetActive(false);
    }

    public void Play()
    {
        foreach (var effect in _effects)
            effect.Play();
    }
}
