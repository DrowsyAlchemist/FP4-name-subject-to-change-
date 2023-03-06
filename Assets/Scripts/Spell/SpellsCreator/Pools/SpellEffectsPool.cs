using UnityEngine;

public class SpellEffectsPool : MonoBehaviour
{
    [SerializeField] private int _initialSize = 6;

    [SerializeField] private GameObject _lifeEffectTemplate;
    [SerializeField] private GameObject _hitEffectTemplate;
    [SerializeField] private GameObject _explosionEffectTemplate;

    private GameObject[] _lifeEffectPool;
    private GameObject[] _hitEffectPool;
    private GameObject[] _explosionEffectPool;

    private void Awake()
    {
        InitPool(_lifeEffectTemplate, out _lifeEffectPool);
        InitPool(_hitEffectTemplate, out _hitEffectPool);
        InitPool(_explosionEffectTemplate, out _explosionEffectPool);
    }

    public GameObject GetLifeEffect()
    {
        foreach (GameObject lifeEffect in _lifeEffectPool)
            if (lifeEffect.gameObject.activeSelf == false)
                return lifeEffect;

        return GetNewEffect(_lifeEffectTemplate, ref _lifeEffectPool);
    }

    public GameObject GetHitEffect()
    {
        foreach (GameObject hitEffect in _hitEffectPool)
            if (hitEffect.gameObject.activeSelf == false)
                return hitEffect;

        return GetNewEffect(_hitEffectTemplate, ref _hitEffectPool);
    }

    public GameObject GetExplosionEffect()
    {
        foreach (GameObject explosionEffect in _explosionEffectPool)
            if (explosionEffect.gameObject.activeSelf == false)
                return explosionEffect;

        return GetNewEffect(_explosionEffectTemplate, ref _explosionEffectPool);
    }

    private void InitPool(GameObject template, out GameObject[] pool)
    {
        pool = new GameObject[_initialSize];

        for (int i = 0; i < _initialSize; i++)
        {
            GameObject effect = Instantiate(template, gameObject.transform);
            pool[i] = effect;
        }
    }

    private GameObject GetNewEffect(GameObject effectTemplate, ref GameObject[] pool)
    {
        int newSize = pool.Length + 1;
        GameObject[] newPool = new GameObject[newSize];

        for (int i = 0; i < pool.Length; i++)
            newPool[i] = pool[i];

        GameObject newEffect = Instantiate(effectTemplate, gameObject.transform);
        newEffect.gameObject.SetActive(false);
        newPool[newSize - 1] = newEffect;
        pool = newPool;
        return newEffect;
    }
}
