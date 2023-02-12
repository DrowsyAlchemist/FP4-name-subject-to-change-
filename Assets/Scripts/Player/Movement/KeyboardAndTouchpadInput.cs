using UnityEngine;
using UnityEngine.EventSystems;

public class KeyboardAndTouchpadInput : PlayerInput, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Transform _player;

    private Vector3 _direction;
    private bool _isDragging;

    public override Vector3 GetDirection()
    {
        if (_isDragging)
        {
            return _direction;
        }

        if (Input.GetKey(KeyCode.D))
            return Vector3.right;

        if (Input.GetKey(KeyCode.A))
            return Vector3.left;

        return Vector3.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _direction = eventData.delta.x > 0 ? Vector3.right : Vector3.left;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
    }
}