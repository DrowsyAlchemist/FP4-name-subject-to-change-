using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Player : MonoBehaviour
{
    [SerializeField] private SpellCaster _spellCaster;

    private Mover _mover;

    public SpellCaster SpellCaster=> _spellCaster;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    public void Play()
    {
        _mover.StartMovement();
        _spellCaster.StartCasting();
    }

    public void StopPlaying()
    {
        _mover.StopMovement();
        _spellCaster.StopCasting();
    }
}