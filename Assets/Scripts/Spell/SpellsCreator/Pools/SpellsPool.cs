using UnityEngine;

public class SpellsPool : MonoBehaviour
{
    [SerializeField] private int _initialSize = 6;
    [SerializeField] private Spell _spellTemplate;

    private Spell[] _pool;

    private void Awake()
    {
        _pool = new Spell[_initialSize];

        for (int i = 0; i < _initialSize; i++)
        {
            var spell = Instantiate(_spellTemplate, gameObject.transform);
            _pool[i] = spell;
        }
    }

    public Spell GetSpell()
    {
        foreach (Spell spell in _pool)
            if (spell.gameObject.activeSelf == false)
                return spell;

        return GetNewSpell();
    }

    private Spell GetNewSpell()
    {
        int newSize = _pool.Length + 1;
        Spell[] newPool = new Spell[newSize];

        for (int i = 0; i < _pool.Length; i++)
            newPool[i] = _pool[i];

        Spell newSpell = Instantiate(_spellTemplate, gameObject.transform);
        newSpell.gameObject.SetActive(false);
        newPool[newSize - 1] = newSpell;
        _pool = newPool;
        return newSpell;
    }
}
