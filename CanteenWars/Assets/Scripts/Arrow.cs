using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    public bool IsTurnedOn;
    

    void Awake ()
	{
	    _animator = GetComponent<Animator>();
	    _spriteRenderer = GetComponent<SpriteRenderer>();
	}

    void Start()
    {
        _animator.SetBool("Rotating", true);
    }
	
    public Vector3 StopRotating()
    {
        var arrowRotation = transform.rotation.eulerAngles;

        _animator.SetBool("Rotating", false);
        _animator.SetBool("Curving", true);
        transform.eulerAngles = arrowRotation;

        return arrowRotation;
    }

    public Vector3 StopCurve()
    {
        var arrowScale = transform.localScale;

        transform.localScale = arrowScale;
        _animator.SetBool("Curving", false);

        return arrowScale;
    }

    public void TurnOffArrow()
    {
        _spriteRenderer.enabled = false;
        _animator.SetBool("Curving", false);
        _animator.SetBool("Rotating", true);
        IsTurnedOn = false;
    }
    public void TurnOnArrow()
    {
        _spriteRenderer.enabled = true;
        IsTurnedOn = true;
    }

    public bool IsCruving()
    {
        return _animator.GetBool("Curving");
    }
    public bool IsRotating()
    {
        return _animator.GetBool("Rotating");
    }
}
