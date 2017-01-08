using System;
using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public Ball ball;
    private Vector3 _offset;

    public float ENDPOINT = 1829;

    // Use this for initialization
    void Start ()
    {
        _offset = transform.position - ball.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (ball.transform.position.z <= ENDPOINT)
	    {
            transform.position = _offset + ball.transform.position;
        }
    }
}
