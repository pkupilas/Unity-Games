using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private Animator _animator;
    

    void Start ()
	{
	    _animator = GetComponent<Animator>();
	}
	
    public Vector3 StopRotating()
    {
        var arrowRotation = transform.rotation.eulerAngles;

        _animator.SetTrigger("CurveArrow");
        transform.eulerAngles = arrowRotation;

        return arrowRotation;
    }

    public Vector3 StopCurve()
    {
        var arrowScale = transform.localScale;

        transform.localScale = arrowScale;
        _animator.SetTrigger("RotateArrow");

        return arrowScale;
    }
}
