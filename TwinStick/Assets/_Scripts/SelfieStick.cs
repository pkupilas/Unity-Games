using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class SelfieStick : MonoBehaviour
{

    public float panSpeed = 2f;

    private GameObject _player;
    private Vector3 armRotation;


    // Use this for initialization
    void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
        armRotation = transform.rotation.eulerAngles;
    }
	
	// Update is called once per frame
	void Update ()
	{

	    armRotation.y += CrossPlatformInputManager.GetAxis("RHorizontal")*panSpeed;
	    armRotation.x += CrossPlatformInputManager.GetAxis("RVertical")* panSpeed;
	    transform.position = _player.transform.position;
        transform.rotation = Quaternion.Euler(armRotation);

	}
}
