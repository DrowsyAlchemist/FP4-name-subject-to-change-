using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Collider))]
public abstract class CombatSpell : UpgradeableSpell
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private ElementType _element;
    [SerializeField] private GameObject _hitEffectTemplate;

    private Mover _mover;

    protected int Damage => _damage;
    protected ElementType Element => _element;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(_hitEffectTemplate, transform.position, Quaternion.identity, null);
        Hit(other);
    }

    protected abstract void Hit(Collider collider);

    public void Launch()
    {
        _mover.StartMovement();
    }

    private void OnValidate()
    {
        GetComponent<Collider>().isTrigger = true;
    }
}