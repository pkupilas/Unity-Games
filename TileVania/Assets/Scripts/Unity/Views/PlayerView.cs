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
    private bool _ShouldJump = false;

    private const string _RUN_PARAMETER_NAME = "IsRunning";
    private const string _GROUND_LAYER_NAME = "Ground";

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
        _Collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        SetInput();
        SetAnimatorParameters();
        Move();
        FlipCharacter();
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
        _ShouldJump = hInput.GetButtonDown("Jump") && _Collider.IsTouchingLayers(LayerMask.GetMask(_GROUND_LAYER_NAME));
    }

    private void SetAnimatorParameters()
    {
        _Animator.SetBool(_RUN_PARAMETER_NAME, IsPlayerRunning());
    }

    private bool IsPlayerRunning()
    {
        return Mathf.Abs(_HorizontalInput) > Mathf.Epsilon;
    }

    private void Move()
    {
        transform.position += Vector3.right * _HorizontalInput * Time.deltaTime * _MovementConfiguration.MovementSpeed;
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
