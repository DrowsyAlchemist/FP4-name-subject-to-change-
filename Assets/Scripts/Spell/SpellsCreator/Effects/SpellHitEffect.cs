using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellHitEffect : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _effects;

    private const float SleepTime = 1;

    public void Play()
    {
        foreach (var effect in _effects)
            effect.Play();

        transform.SetParent(null);

        if (gameObject.activeSelf)
            StartCoroutine(CheckParent());
    }

    private IEnumerator CheckParent()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(SleepTime);

            if (_effects[0].IsAlive() == false)
                gameObject.SetActive(false);
        }
    }
}
