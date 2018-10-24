using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField]
    private MovementConfiguration _MovementConfiguration;
    private Rigidbody2D _Rigidbody;
    private float _HorizontalInput = 0.0f;
    private bool _ShouldJump = false;

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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

    private void Move()
    {
        _HorizontalInput = hInput.GetAxis("Horizontal");
        _ShouldJump = hInput.GetButtonDown("Jump");

        transform.position += Vector3.right * _HorizontalInput * Time.deltaTime * _MovementConfiguration.MovementSpeed;
    }

    private void FlipCharacter()
    {
        if (Mathf.Abs(_HorizontalInput) < Mathf.Epsilon)
            return;

        transform.localScale = new Vector3(
            Mathf.Abs(transform.localScale.x) * Mathf.Sign(_HorizontalInput),
            transform.localScale.y,
            transform.localScale.z
            );
    }
}
