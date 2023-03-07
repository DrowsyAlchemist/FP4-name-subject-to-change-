using UnityEngine;

public abstract class Pool : MonoBehaviour
{
    [SerializeField] private int _initialSize = 6;

    private GameObject _objTemplate;
    private GameObject[] _pool;

    protected void Init(GameObject template)
    {
        _objTemplate = template ?? throw new System.ArgumentNullException();
        _pool = new GameObject[_initialSize];

        for (int i = 0; i < _initialSize; i++)
        {
            var obj = Instantiate(_objTemplate, gameObject.transform);
            _pool[i] = obj;
            obj.SetActive(false);
        }
    }

    protected GameObject GetObject()
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

        var newObj = Instantiate(_objTemplate, gameObject.transform);
        newObj.gameObject.SetActive(false);
        newPool[newSize - 1] = newObj;
        _pool = newPool;
        Debug.Log("New object:" + newObj);
        return newObj;
    }
}