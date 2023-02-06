using System.Collections.Generic;
using UnityEngine;

public class StoreMenu : MonoBehaviour
{
    [SerializeField] private List<UpgradeableSpellData> _waresData;
    [SerializeField] private WareRenderer _wareRendererTemplate;
    [SerializeField] private RectTransform _waresContainer;
    [SerializeField] private AvailableSpellsHolder _playerSpellsHolder;
    [SerializeField] private Mana _playerMana;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private void OnDestroy()
    {
        foreach (var wareRenderer in _waresContainer.GetComponentsInChildren<WareRenderer>())
            wareRenderer.BuyButtonClicked -= OnBuyButtonClick;
    }

    public void Fill()
    {
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

            if (renderer.WareData.UpgradeLevel == 0)
                _playerSpellsHolder.AddSpell(renderer.WareData);

            ware.Upgrade();
        }
    }
}