using System;
using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    private Ball _ball;
    private Vector3 _offset;
    private float ENDPOINT = 1829;

    // Use this for initialization
    void Start ()
    {
        _ball = FindObjectOfType<Ball>();
        _offset = transform.position - _ball.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (_ball.transform.position.z <= ENDPOINT)
	    {
            transform.position = _offset + _ball.transform.position;
        }
    }
}
