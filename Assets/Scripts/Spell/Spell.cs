using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
public class Spell : MonoBehaviour
{
    [SerializeField] private SpellLifeEffect _lifeEffect;
    [SerializeField] private float _secondLevelReduseSizeModifier = 0.75f;
    [SerializeField] private float _explosionRadius = 2.3f;
    [SerializeField] private LayerMask _targetLayers;
    [SerializeField] private float _thirdLevelShieldDamageModifier = 1.5f;
    [SerializeField] private float _regularScale = 0.4f;
    [SerializeField] private float _secondLevelScale = 0.6f;

    private SpellData _resources;

    private Mover _mover;
    private MeshRenderer _meshRenderer;
    private SpellHitEffect _hitEffect;
    private int _hitCount;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _mover = GetComponent<Mover>();
        Game.Instance.LevelCompleted += () => gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _hitCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        _hitEffect.Play();
        Hit(other);
    }

    public void ResetSpell(SpellData resources, SpellHitEffect hitEffect)
    {
        _resources = resources ?? throw new System.ArgumentNullException();
        _meshRenderer.material = resources.Material;
        _lifeEffect.SetColor(resources.EffectColor);

        if (resources.UpgradeLevel == 2)
            transform.localScale = _secondLevelScale * Vector3.one;
        else
            transform.localScale = _regularScale * Vector3.one;

        _hitEffect = hitEffect;
        hitEffect.transform.SetParent(gameObject.transform);
        hitEffect.transform.position = transform.position;
    }

    public void Launch()
    {
        _mover.StartMovement();
    }

    private void Hit(Collider other)
    {
        switch (_resources.UpgradeLevel)
        {
            case 1:
                HitFirstLevel(other);
                break;
            case 2:
                HitSecondLevel(other);
                break;
            case 3:
                HitThirdLevel(other);
                break;
            case 4:
                HitFourthLevel(other);
                break;
        }
    }

    private void HitFirstLevel(Collider other)
    {
        if (other.TryGetComponent(out ITakeDamage subject))
            subject.TakeDamage(_resources.GetDamage(), _resources.Element);

        gameObject.SetActive(false);
    }

    private void HitSecondLevel(Collider other)
    {
        if (other.TryGetComponent(out ITakeDamage subject))
        {
            _hitCount++;
            subject.TakeDamage(_resources.GetDamage() / _hitCount, _resources.Element);
            transform.localScale *= _secondLevelReduseSizeModifier;

            if (other.TryGetComponent(out MagicShield _))
                gameObject.SetActive(false);
        }
    }

    private void HitThirdLevel(Collider other)
    {
        ApplyDamageInRange(other, shieldDamageModifier: 1);
    }

    private void HitFourthLevel(Collider other)
    {
        ApplyDamageInRange(other, _thirdLevelShieldDamageModifier);
    }

    private void ApplyDamageInRange(Collider other, float shieldDamageModifier)
    {
        if (other.TryGetComponent(out MagicShield shield))
        {
            shield.TakeDamage(_resources.GetDamage() * shieldDamageModifier, _resources.Element);
        }
        else
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius, _targetLayers);

            if (hits.Length > 0)
                foreach (Collider hit in hits)
                    if ((1 << hit.gameObject.layer & _targetLayers) > 0)
                        if (hit.TryGetComponent(out ITakeDamage target))
                            target.TakeDamage(_resources.GetDamage(), _resources.Element);
        }
        gameObject.SetActive(false);
    }    

    private void OnValidate()
    {
        GetComponent<Collider>().isTrigger = true;
    }
}