using Characters.Player;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Player _player;

	void Start ()
	{
	    _player = FindObjectOfType<Player>();
	}
	
	void LateUpdate ()
	{
	    transform.position = _player.transform.position;
	}
}