using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerView : MonoBehaviour
{
    [SerializeField]
    private MovementConfiguration _MovementConfiguration;
    private Rigidbody2D _Rigidbody;
    private CapsuleCollider2D _CapsuleCollider;
    private BoxCollider2D _BoxCollider;
    private Animator _Animator;
    private float _HorizontalInput = 0.0f;
    private float _VerticalInput = 0.0f;
    private bool _ShouldJump = false;
    private float _StartingGravityScale = 0.0f;
    private bool _IsDead = false;
    [SerializeField]
    private Vector2 DeathKick = Vector2.zero;

    public delegate void PlayerAction();
    public static event PlayerAction OnDead;

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
        _CapsuleCollider = GetComponent<CapsuleCollider2D>();
        _BoxCollider = GetComponent<BoxCollider2D>();
        _StartingGravityScale = _Rigidbody.gravityScale;
    }

    private void Update()
    {
        Die();

        if (_IsDead)
            return;

        SetInput();
        SetAnimatorParameters();
        Move();
        ClimbLadders();
        Jump();
        FlipCharacter();
    }

    private void SetInput()
    {
        _HorizontalInput = hInput.GetAxis("Horizontal");
        _VerticalInput = hInput.GetAxis("Vertical");
        _ShouldJump = hInput.GetButtonDown("Jump") && _BoxCollider.IsTouchingLayers(LayerMask.GetMask(Utilities.Constans.Layer.GROUND_LAYER_NAME));
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
        return _BoxCollider.IsTouchingLayers(LayerMask.GetMask(Utilities.Constans.Layer.LADDER_LAYER_NAME));
    }

    private void Move()
    {
        _Rigidbody.velocity = new Vector2(_HorizontalInput * Time.deltaTime * _MovementConfiguration.MovementSpeed, _Rigidbody.velocity.y);
    }

    private void ClimbLadders()
    {
        if (IsPlayerOnLadder())
        {
            _Rigidbody.gravityScale = 0.0f;
            _Rigidbody.velocity = new Vector2(_Rigidbody.velocity.x, _VerticalInput * Time.deltaTime * _MovementConfiguration.ClimbSpeed);
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

    private void Jump()
    {
        if (_ShouldJump)
        {
            _Rigidbody.velocity = Vector2.up * _MovementConfiguration.JumpSpeed;
        }
    }

    private void Die()
    {
        if (!_CapsuleCollider.IsTouchingLayers(LayerMask.GetMask(Utilities.Constans.Layer.ENEMY_LAYER_NAME, Utilities.Constans.Layer.HAZARD_LAYER_NAME)))
            return;
        
        _IsDead = true;
        _Animator.SetBool(Utilities.Constans.Animator.DEAD_PARAMETER_NAME, _IsDead);
        _Rigidbody.velocity = DeathKick;

        if(OnDead != null)
            OnDead();
    }
}
