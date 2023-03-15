using UnityEngine;

public class StartNewGameButton : UIButton
{
    [SerializeField] private Game _game;

    protected override void OnButtonClick()
    {
        _game.StartNewGame();
    }
}