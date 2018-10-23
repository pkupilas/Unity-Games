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
        _HorizontalInput = hInput.GetAxis("Horizontal");
        _ShouldJump = hInput.GetButtonDown("Jump");

        transform.position += Vector3.right * _HorizontalInput * Time.deltaTime * _MovementConfiguration.MovementSpeed;
    }

    private void FixedUpdate()
    {
        if (_ShouldJump)
        {
            _Rigidbody.velocity = Vector2.up * _MovementConfiguration.JumpSpeed;
        }
    }
}
