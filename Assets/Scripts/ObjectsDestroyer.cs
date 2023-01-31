using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ObjectsDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    private void OnValidate()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
}