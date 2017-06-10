using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Animator _animator;

	// Use this for initialization
	void Start ()
	{
	    _animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update ()
	{

	}
    
    public void StopRotating()
    {
        var arrowRotation = transform.rotation.eulerAngles;
        _animator.SetTrigger("CurveArrow");
        transform.eulerAngles = arrowRotation;
    }

    public void StopCurve()
    {
        var arrowScale = transform.localScale;
        transform.localScale = arrowScale;
        _animator.Stop();
    }
}
