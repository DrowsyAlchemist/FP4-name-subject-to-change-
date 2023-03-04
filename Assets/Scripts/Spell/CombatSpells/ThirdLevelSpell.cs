using UnityEngine;

public class ThirdLevelSpell : CombatSpell
{
    [SerializeField] private LayerMask _targetLayers;
    [SerializeField] private float _explosionRadius = 2.2f;

    protected override void Hit(Collider collider)
    {
        #region DegugRegion
#if UNITY_EDITOR
        Debug.DrawRay(transform.position, _explosionRadius * Vector3.back, Color.yellow, 1);
        Debug.DrawRay(transform.position, _explosionRadius * Vector3.forward, Color.yellow, 1);
        Debug.DrawRay(transform.position, _explosionRadius * Vector3.left, Color.yellow, 1);
        Debug.DrawRay(transform.position, _explosionRadius * Vector3.right, Color.yellow, 1);
        Debug.DrawRay(transform.position, _explosionRadius * Vector3.up, Color.yellow, 1);
        Debug.DrawRay(transform.position, _explosionRadius * Vector3.down, Color.yellow, 1);
#endif
        #endregion

        if (collider.TryGetComponent(out MagicShield shield))
        {
            shield.TakeDamage(Damage, Element);
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
        Destroy(gameObject);
    }
}