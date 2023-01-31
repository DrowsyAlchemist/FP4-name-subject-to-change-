using UnityEngine;

public abstract class PlayerInput : MonoBehaviour, IPlayerInput
{
    public abstract Vector3 GetDirection();
}
