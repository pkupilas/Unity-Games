using Characters.Player;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Player _player;


	// Use this for initialization
	void Start ()
	{
	    _player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
	    transform.position = _player.transform.position;
	}
}
