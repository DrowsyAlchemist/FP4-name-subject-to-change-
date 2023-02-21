using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private Transform _castPoint;

    private CombatSpell _spellTemplate;

    private void Awake()
    {
        enabled = false;
    }

    public void SetSpell(CombatSpell spellTemplate)
    {
        _spellTemplate = spellTemplate ?? throw new System.ArgumentNullException();
    }

    public void CastSpell()
    {
        var spell = Instantiate(_spellTemplate, _castPoint.position, Quaternion.identity, null);
        spell.Launch();
    }
}