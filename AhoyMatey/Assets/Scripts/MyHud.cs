using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyHud : MonoBehaviour
{

    private NetworkManager _networkManager;

	// Use this for initialization
	void Start ()
	{
	    _networkManager = GetComponent<NetworkManager>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MyStartHost()
    {
        Debug.Log("Starting host at: " + Time.timeSinceLevelLoad);
        _networkManager.StartHost();
    }

    void OnStartHost()
    {
        Debug.Log("Starting host at: " + Time.timeSinceLevelLoad);
    }

}
