using System;
using UnityEngine;
using System.Collections;

public class Daycycle : MonoBehaviour
{
    [Tooltip("Number of minutes per second. ")]
    public float timeScale = 60f;
    
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
        RotateSun();
	}

    private void RotateSun()
    {
        var anglePerFrame = Time.deltaTime / 360f * timeScale;
        transform.RotateAround(transform.position,Vector3.forward,anglePerFrame);
    }
}
