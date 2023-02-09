using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    [SerializeField] private SpellCaster _spellCaster;

    private Mover _mover;
    private PlayerAnimator _animator;

    public SpellCaster SpellCaster=> _spellCaster;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _animator = GetComponent<PlayerAnimator>();
    }

    public void Play()
    {
        _mover.StartMovement();
        _spellCaster.StartCasting();
        _animator.PlayCastSpell();
    }

    public void StopPlaying()
    {
        _mover.StopMovement();
        _spellCaster.StopCasting();
        _animator.PlayIdle();
    }
}