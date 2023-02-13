using UnityEngine;

public class Element : MonoBehaviour
{
    [SerializeField] private float _interactionIncreaseModifier = 2;

    [SerializeField] private Sprite _light;
    [SerializeField] private Sprite _fire;
    [SerializeField] private Sprite _wind;
    [SerializeField] private Sprite _lightning;
    [SerializeField] private Sprite _earth;
    [SerializeField] private Sprite _water;

    private const int MaxElementsDifference = 4;

    private static Element _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public static float GetInteractionModifier(ElementType transmitting, ElementType receiving)
    {
        if (transmitting is ElementType.Light || receiving is ElementType.Light)
            return 1;

        int absDifference = Mathf.Abs(receiving - transmitting);
        bool isDifferencePositive = receiving - transmitting > 0;

        if (absDifference == 0)
            return 1;

        if (absDifference > MaxElementsDifference)
            throw new System.NotImplementedException();

        float reductionModifier = 1 / _instance._interactionIncreaseModifier;

        if (absDifference % 2 == 1)
            return isDifferencePositive ? _instance._interactionIncreaseModifier : reductionModifier;
        else
            return isDifferencePositive ? reductionModifier : _instance._interactionIncreaseModifier;
    }

    public static Sprite GetElementSprite(ElementType element)
    {
        return element switch
        {
            ElementType.Light => _instance._light,
            ElementType.Fire => _instance._fire,
            ElementType.Wind => _instance._wind,
            ElementType.Lightning => _instance._lightning,
            ElementType.Earth => _instance._earth,
            ElementType.Water => _instance._water,
            _ => throw new System.NotImplementedException(),
        };
    }
}