using UnityEngine;

public class ShieldCreator : MonoBehaviour
{
    [SerializeField] private MagicShield _shieldTemplate;
    [SerializeField] private MagicShieldData _data;

    private const int MinElementType = 1;
    private const int MaxElementType = 5;
    private static ShieldCreator _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public static MagicShield CreateRandomShield()
    {
        var shield = Instantiate(_instance._shieldTemplate);
        var element = (ElementType)(Random.Range(MinElementType, MaxElementType + 1));
        var material = _instance._data.GetMaterial(element);
        shield.Init(element, material);
        return shield;
    }
}
