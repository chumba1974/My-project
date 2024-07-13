using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _horizontalMovementMultiplier;
    [SerializeField] private float _verticalMovementMultiplier;

    private Vector3 _targetPosition => _target.position;
    private Vector3 _lastTargetPosition;

    private void Start()
    {
        _lastTargetPosition = _targetPosition;
    }

    private void Update()
    {
        Vector3 delta = _targetPosition - _lastTargetPosition;
        delta *= new Vector2(_horizontalMovementMultiplier, _verticalMovementMultiplier);
        transform.position += delta;
        _lastTargetPosition = _targetPosition;
    }
}
