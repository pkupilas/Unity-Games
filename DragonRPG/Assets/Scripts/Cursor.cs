using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{

    private CameraRaycaster _cameraRaycaster;

	// Use this for initialization
	void Start ()
	{
	    _cameraRaycaster = GetComponent<CameraRaycaster>();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
}
