using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class Player : NetworkBehaviour
{

    private Vector3 _inputValue;
    
	// Update is called once per frame
	void Update ()
	{

	    if (!isLocalPlayer)
	    {
	        return;
	    }

	    _inputValue.x = CrossPlatformInputManager.GetAxis("Horizontal");
	    _inputValue.y = 0f;
        _inputValue.z = CrossPlatformInputManager.GetAxis("Vertical");

        transform.Translate(_inputValue);
    }

    public override void OnStartLocalPlayer()
    {
        var thisCamera = GetComponentInChildren<Camera>();
        thisCamera.enabled = true;
    }

}
