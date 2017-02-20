using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject gun;
    public GameObject ammunition;
    private Transform _transform;

	// Use this for initialization
	void Start ()
	{
	    _transform = GetComponent<Transform>(); // possible nullptr 
	}
	
	// Update is called once per frame
	void Update () {
	    HandleMovement();
	    HandleRotation();
	    HandleFire();
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

    private void HandleRotation()
    {
        //TODO: Improve messy rotation code
        //Debug.Log(_transform.rotation.eulerAngles);
        var mousePosition = Input.mousePosition;
        var mouseRotation = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mousePosition.z - _transform.position.z));

        if ((_transform.position.x != mousePosition.y) && (_transform.position.y != mousePosition.y))
        {
            _transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2((mouseRotation.y - _transform.position.y), (mouseRotation.x - _transform.position.x)) * Mathf.Rad2Deg + 90), 2.0f * Time.deltaTime);
        }
    }

    private void HandleFire()
    {
        const int LEFT_MOUSE_BUTTON = 0;
        if (Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON))
        {
            GameObject newBullet = Instantiate(ammunition);
            newBullet.transform.position = _transform.position;

        }
    }
    

}
