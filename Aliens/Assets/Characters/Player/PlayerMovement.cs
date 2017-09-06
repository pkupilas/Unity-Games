using UnityEngine;

namespace Characters.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private float _speed = 20f;
        private Rigidbody _rigidbody;
        private LayerMask _floor;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _floor = LayerMask.GetMask("Floor");
        }

        void FixedUpdate()
        {
            LookAtCursor();
            Move();
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
                var newRotation = Quaternion.LookRotation(playerToMouse);
                _rigidbody.MoveRotation(newRotation);
            }
        }

        private void Move()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            var distance = new Vector3(h, 0f, v);
            distance = distance.normalized * _speed * Time.deltaTime;
            _rigidbody.MovePosition(transform.position + distance);
        }
    }
}