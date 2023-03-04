using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Collider))]
public abstract class CombatSpell : Spell
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private ElementType _element;
    [SerializeField] private GameObject _hitEffectTemplate;

    private Game _game;
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

    private void OnDestroy()
    {
        _game.LevelStarted -= OnLevelStarted;
    }

    protected abstract void Hit(Collider collider);

    public void Init(Game game)
    {
        _game = game;
        game.LevelStarted += OnLevelStarted;
    }

    public void Launch()
    {
        _mover.StartMovement();
    }

    private void OnLevelStarted()
    {
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        GetComponent<Collider>().isTrigger = true;
    }
}