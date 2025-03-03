using System.Collections.Generic;
using UnityEngine;

public class StoreMenu : MonoBehaviour
{
    [SerializeField] private SpellData _defaultCombatSpell;
    [SerializeField] private List<SpellData> _waresData;
    [SerializeField] private WareRenderer _wareRendererTemplate;
    [SerializeField] private RectTransform _waresContainer;
    [SerializeField] private AvailableSpellsHolder _playerSpellsHolder;
    [SerializeField] private Mana _playerMana;

    private List<WareRenderer> _wareRenderers = new List<WareRenderer>();

    private void OnDestroy()
    {
        foreach (var wareRenderer in _waresContainer.GetComponentsInChildren<WareRenderer>())
            wareRenderer.BuyButtonClicked -= OnBuyButtonClick;
    }

    public void ResetStore()
    {
        foreach (var wareData in _waresData)
            wareData.ResetSpell();

        while (_wareRenderers.Count > 0)
        {
            Destroy(_wareRenderers[0].gameObject);
            _wareRenderers.RemoveAt(0);
        }
        _defaultCombatSpell.ResetSpell();
        _defaultCombatSpell.Upgrade();
        AddWare(_defaultCombatSpell);
        _playerSpellsHolder.SetDefaultSpell(_defaultCombatSpell);

        foreach (var wareData in _waresData)
            AddWare(wareData);
    }

    private void AddWare(SpellData wareData)
    {
        var renderer = Instantiate(_wareRendererTemplate, _waresContainer);
        renderer.Render(wareData);
        renderer.BuyButtonClicked += OnBuyButtonClick;
        _wareRenderers.Add(renderer);
    }

    private void OnBuyButtonClick(WareRenderer renderer)
    {
        SpellData ware = renderer.WareData;
        int cost = ware.GetNextLevelCost();

        if (_playerMana.ManaStorage.CanGiveMana(cost))
        {
            Sound.SellSound.Play();
            _playerMana.ManaStorage.GiveMana(cost);
            ware.Upgrade();

            if (renderer.WareData.UpgradeLevel == 1)
                _playerSpellsHolder.AddSpell(renderer.WareData);
        }
    }
}