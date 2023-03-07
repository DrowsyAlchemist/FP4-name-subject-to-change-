using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private SpellsCreator _spellsCreator;
    [SerializeField] private Transform _castPoint;
    [SerializeField] private AudioSource _castSound;

    private ElementType _currentSpellElement;

    private void Awake()
    {
        enabled = false;
    }

    public void SetSpell(ElementType spellElement)
    {
        _currentSpellElement = spellElement;
    }

    public void CastSpell()
    {
        var spell = _spellsCreator.Create(_currentSpellElement);
        spell.transform.position = _castPoint.position;
        spell.Launch();
        _castSound.Play();
    }
}