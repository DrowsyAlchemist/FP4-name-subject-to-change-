using TMPro;
using UnityEngine;

public class IncreaseWallMaxHealthButton : UIButton
{
    [SerializeField] private int _initialCost;
    [SerializeField] private int _coastIncrease;
    [SerializeField] private float _extraHealth;
    [SerializeField] private Game _game;
    [SerializeField] private Wall _wall;
    [SerializeField] private Mana _playerMana;

    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _extraHealthText;

    private void Awake()
    {
        _game.LevelFinished += () => Button.interactable = true;
        _wall.Destroyed += () => Button.interactable = false;
        _extraHealthText.text = "+" + _extraHealth + " hp";
        _costText.text = _initialCost.ToString();
    }

    protected override void OnButtonClick()
    {
        if (_playerMana.ManaStorage.CanGiveMana(_initialCost))
        {
            Button.interactable = false;
            _playerMana.ManaStorage.GiveMana(_initialCost);
            _wall.IncreaseMaxHealth(_extraHealth);
            _initialCost += _coastIncrease;
            _costText.text = _initialCost.ToString();
        }
    }
}