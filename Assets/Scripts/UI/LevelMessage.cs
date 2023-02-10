using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LevelMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private const string ShowAnimation = "Show";
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Show(string message)
    {
        if (IsAnimatorPlaying() == false)
        {
            _text.text = message;
            _animator.Play(ShowAnimation);
        }
        else
        {
            StartCoroutine(ShowWithDelay(message));
        }
    }

    private IEnumerator ShowWithDelay(string message)
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        _text.text = message;
        _animator.Play(ShowAnimation);
    }

    private bool IsAnimatorPlaying()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName(ShowAnimation);
    }
}