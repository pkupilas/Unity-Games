using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour
{
    
    private bool isCalled = false;
    private Rigidbody _rigidbody;

    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
    }
	
    private void OnDispatchHelicopter()
    {
        isCalled = true;
        AddVelocity();
    }

    private void AddVelocity()
    {
        _rigidbody.velocity = new Vector3(0, 0, 50f);
    }
}
