using TMPro;
using UnityEngine;

public class ManaRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _amountText;

    private ManaStorage _manaStorage;

    private void OnDestroy()
    {
        UnsubscribeFromStorage(_manaStorage);
    }

    public void Render(ManaStorage manaStorage)
    {
        UnsubscribeFromStorage(_manaStorage);
        _manaStorage = manaStorage ?? throw new System.ArgumentNullException();
        _amountText.text = manaStorage.Amount.ToString();
        _manaStorage.AmountChanged += OnManaAmountChanged;
    }

    private void OnManaAmountChanged(int amount)
    {
        _amountText.text = amount.ToString();
    }

    private void UnsubscribeFromStorage(ManaStorage storage)
    {
        if (storage != null)
            storage.AmountChanged -= OnManaAmountChanged;
    }
}