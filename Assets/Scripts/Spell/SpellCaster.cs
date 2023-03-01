using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Transform _castPoint;
    [SerializeField] private AudioSource _castSound;

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
        spell.Init(_game);
        spell.Launch();
        _castSound.Play();
    }
}