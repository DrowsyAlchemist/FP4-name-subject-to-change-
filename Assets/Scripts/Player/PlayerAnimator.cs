using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string IdleAnimation = "Idle";
    private const string CastSpellAnimation = "CastSpell";

    public void PlayIdle()
    {
        _animator.Play(IdleAnimation);
    }

    public void PlayCastSpell()
    {
        _animator.Play(CastSpellAnimation);
    }
}