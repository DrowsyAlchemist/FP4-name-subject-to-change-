using System.Collections.Generic;
using UnityEngine;

public class StoreMenu : MonoBehaviour
{
    [SerializeField] private List<UpgradeableSpell> _wares;
    [SerializeField] private WareRenderer _wareRendererTemplate;
    [SerializeField] private RectTransform _waresContainer;
    [SerializeField] private AvailableSpellsHolder _playerSpellsHolder;

    private void OnDestroy()
    {
        foreach (var wareRenderer in _waresContainer.GetComponentsInChildren<WareRenderer>())
            wareRenderer.BuyButtonClicked -= OnBuyButtonClick;
    }

    public void Fill()
    {
        foreach (var ware in _wares)
            AddWare(ware);
    }

    private void AddWare(UpgradeableSpell ware)
    {
        var renderer = Instantiate(_wareRendererTemplate, _waresContainer);
        renderer.Render(ware);
        renderer.BuyButtonClicked += OnBuyButtonClick;
    }

    private void OnBuyButtonClick(WareRenderer renderer)
    {
        if (renderer.Ware.UpgrageLevel == 0)
            _playerSpellsHolder.AddSpell(renderer.Ware);

        renderer.Ware.Upgrade();
    }
}