using UnityEngine;

public class EffectDestroyer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void FixedUpdate()
    {
        if (_particleSystem == null)
            Destroy(gameObject);
    }
}