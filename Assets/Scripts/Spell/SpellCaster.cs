using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private float _secondsBetweenCasts = 0.5f;
    [SerializeField] private Transform _castPoint;

    private CombatSpell _spellTemplate;
    private float _elapsedTime;

    private void Awake()
    {
        enabled = false;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _secondsBetweenCasts)
        {
            _elapsedTime = 0;
            CastSpell();
        }
    }

    public void SetSpell(CombatSpell spellTemplate)
    {
        _spellTemplate = spellTemplate ?? throw new System.ArgumentNullException();
    }

    public void StartCasting()
    {
        if (_spellTemplate == null)
            throw new System.InvalidOperationException();

        enabled = true;
    }

    public void StopCasting()
    {
        enabled = false;
    }

    private void CastSpell()
    {
        var spell = Instantiate(_spellTemplate, _castPoint.position, Quaternion.identity, null);
        spell.Launch();
    }
}