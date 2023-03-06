using UnityEngine;

public abstract class Pool : MonoBehaviour
{
    [SerializeField] private int _initialSize = 6;
    [SerializeField] private GameObject _template;

    private GameObject[] _pool;

    private void Awake()
    {
        _pool = new GameObject[_initialSize];

        for (int i = 0; i < _initialSize; i++)
        {
            var obj = Instantiate(_template, gameObject.transform);
            _pool[i] = obj;
        }
    }

    public GameObject GetObject()
    {
        foreach (var obj in _pool)
            if (obj.gameObject.activeSelf == false)
                return obj;

        return GetNewObject();
    }

    private GameObject GetNewObject()
    {
        int newSize = _pool.Length + 1;
        var newPool = new GameObject[newSize];

        for (int i = 0; i < _pool.Length; i++)
            newPool[i] = _pool[i];

        var newObj = Instantiate(_template, gameObject.transform);
        newObj.gameObject.SetActive(false);
        newPool[newSize - 1] = newObj;
        _pool = newPool;
        return newObj;
    }
}