using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private Transform _player;
    [SerializeField] private Camera _camera;

    private const float Epsilon = 0.2f;

    private bool _isTouchInput;
    private RaycastHit _hit;
    private LayerMask _layerMask;
    private Ray _ray;

    private void Awake()
    {
        _layerMask = 1 << gameObject.layer;
    }

    public Vector3 GetDirection()
    {
        if (_isTouchInput && (Input.touchCount > 0 || Input.GetMouseButton(0)))
            return GetDirectionFromPointer();
        else
            return GetDirectionFromKeyboard();
    }

    private Vector3 GetDirectionFromKeyboard()
    {
        if (Input.GetKey(KeyCode.D))
            return Vector3.right;

        if (Input.GetKey(KeyCode.A))
            return Vector3.left;

        return Vector3.zero;
    }

    private Vector3 GetDirectionFromPointer()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity))
        {
            Vector3 targetPosition = _hit.point;
            float delta = targetPosition.x - _player.position.x;

            if (Mathf.Abs(delta) > Epsilon)
                return delta * Vector3.right;
        }
        return Vector3.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isTouchInput = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
            _isTouchInput = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isTouchInput = false;
    }
}