using UnityEngine;

namespace _Camera
{
    public class CameraFollow : MonoBehaviour
    {

        private GameObject _player;

        void Start () {
            _player = GameObject.FindWithTag("Player");	
        }
	
        void LateUpdate ()
        {
            transform.position = _player.transform.position;
        }
    }
}
