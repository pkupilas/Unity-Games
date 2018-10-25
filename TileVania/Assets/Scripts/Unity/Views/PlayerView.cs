using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class PlayerView : MonoBehaviour
{
    [SerializeField]
    private MovementConfiguration _MovementConfiguration;
    private Rigidbody2D _Rigidbody;
    private Collider2D _Collider;
    private Animator _Animator;
    private float _HorizontalInput = 0.0f;
    private float _VerticalInput = 0.0f;
    private bool _ShouldJump = false;
    private float _StartingGravityScale = 0.0f;


    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
        _Collider = GetComponent<Collider2D>();
        _StartingGravityScale = _Rigidbody.gravityScale;
    }

    private void Update()
    {
        SetInput();
        SetAnimatorParameters();
        Move();
        ClimbLadders();
        FlipCharacter();
        Debug.Log(_HorizontalInput);
        Debug.Log(_VerticalInput);
    }

    private void FixedUpdate()
    {
        if (_ShouldJump)
        {
            _Rigidbody.velocity = Vector2.up * _MovementConfiguration.JumpSpeed;
        }
    }

    private void SetInput()
    {
        _HorizontalInput = hInput.GetAxis("Horizontal");
        _VerticalInput = hInput.GetAxis("Vertical");
        _ShouldJump = hInput.GetButtonDown("Jump") && _Collider.IsTouchingLayers(LayerMask.GetMask(Utilities.Constans.Animator.GROUND_LAYER_NAME));
    }

    private void SetAnimatorParameters()
    {
        _Animator.SetBool(Utilities.Constans.Animator.RUN_PARAMETER_NAME, IsPlayerRunning());
        _Animator.SetBool(Utilities.Constans.Animator.CLIMB_PARAMETER_NAME, IsPlayerOnLadder());
    }

    private bool IsPlayerRunning()
    {
        return Mathf.Abs(_HorizontalInput) > Mathf.Epsilon;
    }

    private bool IsPlayerOnLadder()
    {
        return _Collider.IsTouchingLayers(LayerMask.GetMask(Utilities.Constans.Animator.LADDER_LAYER_NAME));
    }

    private void Move()
    {
        transform.position += Vector3.right * _HorizontalInput * Time.deltaTime * _MovementConfiguration.MovementSpeed;
    }

    private void ClimbLadders()
    {
        if (IsPlayerOnLadder())
        {
            _Rigidbody.gravityScale = 0.0f;
            transform.position += Vector3.up * _VerticalInput * Time.deltaTime * _MovementConfiguration.ClimbSpeed;
        }
        else if(Math.Abs(_Rigidbody.gravityScale - _StartingGravityScale) > Mathf.Epsilon)
        {
            _Rigidbody.gravityScale = _StartingGravityScale;
        }
    }

    private void FlipCharacter()
    {
        if (!IsPlayerRunning())
            return;

        transform.localScale = new Vector3(
            Mathf.Abs(transform.localScale.x) * Mathf.Sign(_HorizontalInput),
            transform.localScale.y,
            transform.localScale.z
            );
    }
}
