using UnityEngine;
using UnityEngine.UI;

public class ElementShower : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Animator _animator;

    private const string ShowAnimation = "Show";

    public void Show(ElementType element)
    {
        if (_image.sprite == null)
            _image.sprite = Element.GetElementSprite(element);

        _animator.Play(ShowAnimation);
    }
}