using System.Collections.Generic;
using UnityEngine;

public class StoreMenu : MonoBehaviour
{
    [SerializeField] private UpgradeableSpellData _defaultSpell;
    [SerializeField] private List<UpgradeableSpellData> _waresData;
    [SerializeField] private WareRenderer _wareRendererTemplate;
    [SerializeField] private RectTransform _waresContainer;
    [SerializeField] private AvailableSpellsHolder _playerSpellsHolder;
    [SerializeField] private Mana _playerMana;

    private void OnDestroy()
    {
        foreach (var wareRenderer in _waresContainer.GetComponentsInChildren<WareRenderer>())
            wareRenderer.BuyButtonClicked -= OnBuyButtonClick;
    }

    public void Fill()
    {
        _defaultSpell.Upgrade();
        AddWare(_defaultSpell);
        _playerSpellsHolder.SetDefaultSpell(_defaultSpell);

        foreach (var wareData in _waresData)
            AddWare(wareData);
    }

    private void AddWare(UpgradeableSpellData wareData)
    {
        var renderer = Instantiate(_wareRendererTemplate, _waresContainer);
        renderer.Render(wareData);
        renderer.BuyButtonClicked += OnBuyButtonClick;
    }

    private void OnBuyButtonClick(WareRenderer renderer)
    {
        UpgradeableSpellData ware = renderer.WareData;
        int cost = ware.GetNextLevelCost();

        if (_playerMana.ManaStorage.CanGiveMana(cost))
        {
            _playerMana.ManaStorage.GiveMana(cost);
            ware.Upgrade();

            if (renderer.WareData.UpgradeLevel == 1)
                _playerSpellsHolder.AddSpell(renderer.WareData);
        }
    }
}