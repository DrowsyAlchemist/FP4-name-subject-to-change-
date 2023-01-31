using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Spell : MonoBehaviour
{
    [SerializeField] private int _damage = 1;

    private Mover _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out ITakeDamage subject))
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
}