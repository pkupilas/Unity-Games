using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private Transform _transform;

	// Use this for initialization
	void Start ()
	{
	    _transform = GetComponent<Transform>(); // possible nullptr 
	}
	
	// Update is called once per frame
	void Update () {
	    HandleMovement();
	}

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _transform.position += new Vector3(-0.1f,0,0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _transform.position += new Vector3(0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            _transform.position += new Vector3(0, 0.1f, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _transform.position += new Vector3(0, -0.1f, 0);
        }
    }


}
