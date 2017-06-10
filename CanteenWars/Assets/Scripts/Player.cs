using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Arrow _arrow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        _arrow.StopRotating();
	    }
        else if (Input.GetKeyUp(KeyCode.Space))
	    {
	        _arrow.StopCurve();
	    }

    }
}
