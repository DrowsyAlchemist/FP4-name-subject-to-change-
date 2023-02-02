using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string IdleAnimation = "Idle";
    private const string WalkAnimation = "Walk";
    private const string AttackAnimation = "Attack";
    private const string HurtAnimation = "Hurt";
    private const string DieAnimation = "Die";

    public float GetCurrentAnimationLength(int layerIndex = 0)
    {
        return _animator.GetCurrentAnimatorStateInfo(layerIndex).length;
    }

    public void PlayIdle()
    {
        _animator.Play(IdleAnimation);
    }

    public void PlayWalk()
    {
        _animator.Play(WalkAnimation);
    }

    public void PlayAttack()
    {
        _animator.Play(AttackAnimation);
    }

    public void PlayHurt()
    {
        _animator.Play(HurtAnimation);
    }

    public void PlayDie()
    {
        _animator.Play(DieAnimation);
    }
}
