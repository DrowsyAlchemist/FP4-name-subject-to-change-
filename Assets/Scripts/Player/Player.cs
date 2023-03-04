using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    [SerializeField] private SpellCaster _spellCaster;
    [SerializeField] private float _secondsBetweenCasts = 0.5f;
    [SerializeField] private float _castAnimationDelay;

    private Mover _mover;
    private PlayerAnimator _animator;
    private float _elapsedTime;

    public SpellCaster SpellCaster=> _spellCaster;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _animator = GetComponent<PlayerAnimator>();
        enabled = false;
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.fixedDeltaTime;

        if (_elapsedTime > _secondsBetweenCasts)
        {
            _elapsedTime = 0;
            _animator.PlayCastSpell();
            StartCoroutine(CastSpellWithDelay(_castAnimationDelay));
        }
    }

    public void Play()
    {
        _mover.StartMovement();
        enabled = true;
    }

    public void StopPlaying()
    {
        enabled = false;
        _mover.StopMovement();
    }

    private IEnumerator CastSpellWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _spellCaster.CastSpell();
    }
}