using UnityEngine;

namespace Characters.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private LayerMask _floor;
        private Player _player;
        private AutoTarget _autoTarget;
        
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _player = GetComponent<Player>();
            _floor = LayerMask.GetMask("Floor");
        }

        void Update()
        {
            _autoTarget = GetComponentInChildren<AutoTarget>();
        }

        void FixedUpdate()
        {
            Move();
            LookAtCursor();
        }

        private void LookAtCursor()
        {
            float _camRayLength = 200f;
            RaycastHit cameraRayHitWithFloor;
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(cameraRay.origin,cameraRay.direction*200f,Color.yellow);
            if (Physics.Raycast(cameraRay, out cameraRayHitWithFloor, _camRayLength, _floor))
            {
                Vector3 playerToMouse = cameraRayHitWithFloor.point - transform.position;
                //Debug.DrawLine(transform.position, cameraRayHitWithFloor.point,Color.magenta);
                playerToMouse.y = 0f;
                if (!_autoTarget || _autoTarget.SpottedEnemy == null)
                {
                    var newRotation = Quaternion.LookRotation(playerToMouse);
                    _rigidbody.MoveRotation(newRotation);
                }
                else
                {
                    var newRotation = Quaternion.LookRotation(_autoTarget.SpottedEnemy.transform.position- transform.position);
                    _rigidbody.MoveRotation(newRotation);
                }
            }
        }

        private void Move()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            float playerSpeed = _player.CharacterData.Speed;
            var distance = new Vector3(h, 0f, v);

            distance = distance.normalized * playerSpeed * Time.deltaTime;
            _rigidbody.MovePosition(transform.position + distance);
        }
    }
}