using UnityEngine;

public abstract class Mover : MonoBehaviour, IMover
{
    protected virtual void Awake()
    {
        enabled = false;
    }

    public void StartMovement()
    {
        enabled = true;
    }

    public void StopMovement()
    {
        enabled = false;
    }
}