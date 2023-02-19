using TMPro;
using UnityEngine;

public class IncreaseWallMaxHealthButton : UIButton
{
    [SerializeField] private int _cost;
    [SerializeField] private float _extraHealth;
    [SerializeField] private Wall _wall;
    [SerializeField] private Mana _playerMana;

    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _extraHealthText;


    private void Awake()
    {
        _extraHealthText.text = "+" + _extraHealth + " hp";
        _costText.text = _cost.ToString();
    }

    protected override void OnButtonClick()
    {
        if (_playerMana.ManaStorage.CanGiveMana(_cost))
        {
            _playerMana.ManaStorage.GiveMana(_cost);
            _wall.IncreaseMaxHealth(_extraHealth);
        }
    }
}