using UnityEngine;

public class TryAgainButton : UIButton
{
    [SerializeField] private Game _game;

    protected override void OnButtonClick()
    {
        _game.TryLevelAgain();
    }
}
