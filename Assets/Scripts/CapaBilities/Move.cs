using BHS;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maxAcceleration;
    [SerializeField] private float _maxAirAcceleration;

    private Controller _controller;

    private Vector2 _direction, _desiredVelocity, _velocity;
    private Rigidbody2D _body;
    private Ground _ground;

    private float _maxSpeedChange, _acceleration;
    private bool _onGrund;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _controller = GetComponent<Controller>();
    }

    private void Update()
    {
        _direction.x = _controller.input.RetrieveMoveInput(); //если сломается поменять input на Input
        _desiredVelocity = new Vector2(_direction.x, 0) * Mathf.Max(_maxSpeed - _ground.Friction, 0f);
    }

    private void FixedUpdate() //использовать его при работе с физикой
    {
        _onGrund = _ground.OnGround;
        _velocity = _body.velocity;

        _acceleration = _onGrund ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration + Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

        _body.velocity = _velocity;
    }

}
