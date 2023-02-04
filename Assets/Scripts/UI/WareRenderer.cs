using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WareRenderer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private Button _buyButton;

    public UpgradeableSpell Ware { get; private set; }

    public event Action<WareRenderer> BuyButtonClicked;

    private void Start()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClick);
    }

    private void OnDestroy()
    {
        _buyButton.onClick.RemoveListener(OnBuyButtonClick);
    }

    public void Render(UpgradeableSpell ware)
    {
        Ware = ware;
        _image.sprite = ware.Data.Sprite;
        _lable.text = ware.Data.Lable;
        _costText.text = ware.Data.Cost.ToString();
    }

    private void OnBuyButtonClick()
    {
        BuyButtonClicked?.Invoke(this);
    }
}