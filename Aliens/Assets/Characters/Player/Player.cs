using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Characters.Player
{
    public class Player : MonoBehaviour
    {
        private float _speed = 10f;

        void Update()
        {
            LookAtCursor();
            Move();
        }

        private void Move()
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

            transform.position += transform.forward * v * Time.deltaTime * _speed + transform.right * h * Time.deltaTime * _speed;
        }

        private void LookAtCursor()
        {
            RaycastHit cameraRayHit;
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cameraRay, out cameraRayHit))
            {
                    var targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                    transform.LookAt(targetPosition);
            }
        }
    }
}
