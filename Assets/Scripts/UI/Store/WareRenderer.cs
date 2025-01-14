using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WareRenderer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _upgradeLevelText;
    [SerializeField] private Button _buyButton;

    public SpellData WareData { get; private set; }

    public event Action<WareRenderer> BuyButtonClicked;

    private void Start()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClick);
    }

    private void OnDestroy()
    {
        _buyButton.onClick.RemoveListener(OnBuyButtonClick);
    }

    public void Render(SpellData wareData)
    {
        WareData = wareData;
        _lable.text = wareData.Lable;
        _image.sprite = wareData.Sprite;
        RenderNextLevelWare(WareData);
    }

    private void RenderNextLevelWare(SpellData wareData)
    {
        int nextLevel = wareData.UpgradeLevel + 1;

        if (nextLevel > wareData.MaxLevel)
        {
            _buyButton.interactable = false;
            return;
        }
        else
        {
            _upgradeLevelText.text = nextLevel.ToString();
            _costText.text = wareData.GetNextLevelCost().ToString();
        }
    }

    private void OnBuyButtonClick()
    {
        BuyButtonClicked?.Invoke(this);
        RenderNextLevelWare(WareData);
    }
}