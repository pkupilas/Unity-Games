using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public virtual float Acceleration { get; set; }
    private Rigidbody2D _rigidbody;


	void Awake ()
	{
	    _rigidbody = GetComponent<Rigidbody2D>();
	    Acceleration = 1000f;
	}
	
    public void Throw(Vector3 transformEulerAngles, Vector3 transformLocalScale)
    {
        var angle = transformEulerAngles.z * Mathf.Deg2Rad;
        var direction = new Vector3(-Mathf.Sin(angle), Mathf.Cos(angle), 0f).normalized;

        _rigidbody.AddForce(direction * transformLocalScale.y * Acceleration);
    }
}
