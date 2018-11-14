using UnityEngine;

enum eMoveDirection
{
    LEFT = -1,
    RIGHT = 1
}

public class EnemyView : MonoBehaviour
{
    [SerializeField]
    private MovementConfiguration _MovementConfiguration;
    [SerializeField]
    private Transform _RaycastOrigin;
    [SerializeField]
    private bool _DrawDebug = false;

    private const float _RaycastDownDistance = 1.0f;
    private const float _RaycastFrontDistance = 0.25f;
    private eMoveDirection _Direction = eMoveDirection.RIGHT;
    private Rigidbody2D _Rigidbody2D;

    private void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var raycastDownHit = Physics2D.Raycast(
            _RaycastOrigin.position,
            Vector2.down,
            _RaycastDownDistance,
            LayerMask.GetMask(Utilities.Constans.Layer.GROUND_LAYER_NAME));

        var raycastForwardHit = Physics2D.Raycast(
            _RaycastOrigin.position,
            Vector2.right * (int) _Direction,
            _RaycastFrontDistance,
            LayerMask.GetMask(Utilities.Constans.Layer.GROUND_LAYER_NAME));

        if (_DrawDebug)
        {
            DebugRaycasts();
        }
        
        if (raycastDownHit.collider != null && raycastForwardHit.collider == null)
            return;

        FlipCharacter();
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void DebugRaycasts()
    {
        Debug.DrawLine(
            _RaycastOrigin.position,
            _RaycastOrigin.position + Vector3.down * _RaycastDownDistance);
        Debug.DrawLine(
            _RaycastOrigin.position,
            _RaycastOrigin.position + Vector3.right * (int)_Direction * _RaycastFrontDistance);
    }

    private void FlipCharacter()
    {
        _Direction = _Direction == eMoveDirection.LEFT ? eMoveDirection.RIGHT : eMoveDirection.LEFT;

        transform.localScale = new Vector3(
            Mathf.Abs(transform.localScale.x) * Mathf.Sign((int) _Direction),
            transform.localScale.y,
            transform.localScale.z
        );
    }
    
    private void Move()
    {
        _Rigidbody2D.velocity =
            new Vector2(_MovementConfiguration.MovementSpeed * Time.deltaTime * (int)_Direction,
                _Rigidbody2D.velocity.y);
    }
}
