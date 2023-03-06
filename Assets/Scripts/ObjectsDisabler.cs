using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ObjectsDisabler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
}