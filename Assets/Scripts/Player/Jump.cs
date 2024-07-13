using BHS;
using UnityEngine;

[RequireComponent(typeof(Controller))]

public class Jump : MonoBehaviour
{
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _maxAirJump;
    [SerializeField] private float _downwardMovementMultiplier;
    [SerializeField] private float _upardMovementMultiplier;

    private Controller _controller;
    private Rigidbody2D _body;
    private Ground _ground;
    private Vector2 _velocity;

    private int _jumpPhase;
    private float defaultGravityScale, _jumpSpeed;

    private bool _desiredJump, _onGround;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _controller = GetComponent<Controller>();

        defaultGravityScale = 1f;
    }

    private void Update()
    {
        _desiredJump |= _controller.input.RetrieveJumpInput();

    }

    private void FixedUpdate()
    {
        _onGround = _ground.OnGround;
        _velocity = _body.velocity;

        if (_onGround)
        {
            _jumpPhase = 0;
        }

        if (_desiredJump)
        {
            _desiredJump = false;
            JumpAction();
        }

        if (_body.velocity.y > 0)
        {
            _body.gravityScale = _upardMovementMultiplier;
        }
        else if(_body.velocity.y < 0)
        {
            _body.gravityScale = _downwardMovementMultiplier;
        }
        else if (_body.velocity.y==0)
        {
            _body.gravityScale = defaultGravityScale;
        }

        _body.velocity = _velocity;
    }

    private void JumpAction()
    {
        if (_onGround || _jumpPhase < _maxAirJump) 
        {
            _jumpPhase++;

            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);

            if(_velocity.y > 0)
            {
                _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0);
            }
            else if(_velocity.y < 0)
            {
                _jumpSpeed += Mathf.Abs(_body.velocity.y);
            }

            _velocity.y += _jumpSpeed;
        }
    }
}
