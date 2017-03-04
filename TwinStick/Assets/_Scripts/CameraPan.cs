using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CameraPan : MonoBehaviour
{

    private GameObject _player;

	// Use this for initialization
	void Start () {
	    _player = GameObject.FindGameObjectWithTag("Player");
	}
	

	void LateUpdate ()
    {
        transform.LookAt(_player.transform);
    }
}
