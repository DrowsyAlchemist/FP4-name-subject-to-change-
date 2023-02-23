using UnityEngine;

public class FourthLevelSpell : CombatSpell
{
    [SerializeField] private LayerMask _targetLayers;
    [SerializeField] private float _explosionRadius = 2;
    [SerializeField] private float _shieldDamageModifier = 1.5f;

    protected override void Hit(Collider collider)
    {
        #region DegugRegion
        Debug.DrawRay(transform.position, _explosionRadius * Vector3.back, Color.yellow, 1);
        Debug.DrawRay(transform.position, _explosionRadius * Vector3.forward, Color.yellow, 1);
        Debug.DrawRay(transform.position, _explosionRadius * Vector3.left, Color.yellow, 1);
        Debug.DrawRay(transform.position, _explosionRadius * Vector3.right, Color.yellow, 1);
        Debug.DrawRay(transform.position, _explosionRadius * Vector3.up, Color.yellow, 1);
        Debug.DrawRay(transform.position, _explosionRadius * Vector3.down, Color.yellow, 1);
        #endregion

        if (collider.TryGetComponent(out MagicShield shield))
        {
            shield.TakeDamage(Damage * _shieldDamageModifier, Element);
        }
        else
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius, _targetLayers);

            if (hits.Length > 0)
                foreach (Collider hit in hits)
                    if ((1 << hit.gameObject.layer & _targetLayers) > 0)
                        if (hit.TryGetComponent(out ITakeDamage target))
                            target.TakeDamage(Damage, Element);
        }
        Collapse();
    }

    private void Collapse()
    {
        Destroy(gameObject);
    }
}
