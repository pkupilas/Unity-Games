using UnityEngine;
using System.Collections;

public class Eyes : MonoBehaviour
{

    private Camera _eyes;
    private float defaultFOV;


	// Use this for initialization
	void Start ()
	{
	    _eyes = GetComponent<Camera>();
	    defaultFOV = _eyes.fieldOfView;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    HandleZooming();
	}

    //TODO: Make more smooth
    private void HandleZooming()
    {
        if (Input.GetButton("Zoom"))
        {
            _eyes.fieldOfView = defaultFOV /  1.5f;
        }
        else
        {
            _eyes.fieldOfView = defaultFOV;
        }
    }
}
