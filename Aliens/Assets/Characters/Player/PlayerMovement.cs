using UnityEngine;
using Weapons;

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

            if (Physics.Raycast(cameraRay, out cameraRayHitWithFloor, _camRayLength, _floor))
            {
                Vector3 playerToMouse = cameraRayHitWithFloor.point - transform.position;
                playerToMouse.y = 0f;
                var newRotation = (!_autoTarget || _autoTarget.SpottedEnemy == null)
                    ? Quaternion.LookRotation(playerToMouse)
                    : Quaternion.LookRotation(_autoTarget.SpottedEnemy.transform.position - transform.position);
                _rigidbody.MoveRotation(newRotation);
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