using UnityEngine;

public class WallRepairSpell : UpgradeableSpell
{
    [SerializeField] private float _restorePercents;

    public void Use()
    {
        Game.Wall.RestoreHealth(_restorePercents);
    }
}
