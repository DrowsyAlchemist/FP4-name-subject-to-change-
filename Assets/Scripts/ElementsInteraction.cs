using UnityEngine;

public static class ElementsInteraction
{
    private const float Modifier = 2;
    private const int MaxDifference = 4;

    public static float GetInteractionModifier(ElementType transmitting, ElementType receiving)
    {
        if (transmitting is ElementType.Default || receiving is ElementType.Default)
            return 1;

        int absDifference = Mathf.Abs(receiving - transmitting);
        bool isDifferencePositive = receiving - transmitting > 0;

        if (absDifference == 0)
            return 1;

        if (absDifference > MaxDifference)
            throw new System.NotImplementedException();

        if (absDifference % 2 == 1)
            return isDifferencePositive ? Modifier : (1 / Modifier);
        else
            return isDifferencePositive ? (1 / Modifier) : Modifier;
    }
}