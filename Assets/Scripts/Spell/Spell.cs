using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Collider))]
public class Spell : MonoBehaviour
{
    [SerializeField] private int _damage = 1;

    private Mover _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ITakeDamage subject))
        {
            subject.TakeDamage(_damage);
            Collapse();
        }
    }

    public void Launch()
    {
        _mover.StartMovement();
    }

    private void Collapse()
    {
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        GetComponent<Collider>().isTrigger = true;
    }
}