using UnityEngine;

public class KeyboardInput : PlayerInput
{
    public override Vector3 GetDirection()
    {
        if (Input.GetKey(KeyCode.D))
            return Vector3.right;

        if (Input.GetKey(KeyCode.A))
            return Vector3.left;

        return Vector3.zero;
    }
}