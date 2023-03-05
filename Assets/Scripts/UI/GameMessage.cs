using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GameMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private const string ShowAnimation = "Show";
    private const string IdleAnimation = "Idle";
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Show(string message)
    {
        _text.text = message;

        if (IsAnimatorPlaying())
            StartCoroutine(ResetAndShow());
        else
            _animator.Play(ShowAnimation);
    }

    private IEnumerator ResetAndShow()
    {
        _animator.Play(IdleAnimation);
        yield return new WaitForEndOfFrame();
        _animator.Play(ShowAnimation);
    }

    private bool IsAnimatorPlaying()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName(ShowAnimation);
    }
}