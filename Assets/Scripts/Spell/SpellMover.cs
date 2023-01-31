using UnityEngine;

public class SpellMover : Mover
{
    [SerializeField] private float _speed = 2;

    private readonly Vector3 _defaultDirection = Vector3.forward;

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * _defaultDirection);
    }
}