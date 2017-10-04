using UnityEngine;
using _Characters.Player;

namespace _Camera
{
    public class CameraFollow : MonoBehaviour
    {
        private PlayerControl _player;

        void Start ()
        {
            _player = FindObjectOfType<PlayerControl>();
        }
	
        void LateUpdate ()
        {
            transform.position = _player.transform.position;
        }
    }
}
