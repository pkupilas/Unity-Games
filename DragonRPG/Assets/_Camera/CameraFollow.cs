using UnityEngine;
using _Characters.Player;

namespace _Camera
{
    public class CameraFollow : MonoBehaviour
    {
        private PlayerMovement _player;

        void Start ()
        {
            _player = FindObjectOfType<PlayerMovement>();
        }
	
        void LateUpdate ()
        {
            transform.position = _player.transform.position;
        }
    }
}
