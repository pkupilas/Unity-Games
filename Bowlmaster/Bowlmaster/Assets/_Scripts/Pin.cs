using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Pin : MonoBehaviour
{

    public float standingThreshold = 3f;
    public float distanceToRaise = 70f;

    private Rigidbody _rigidbody;

    // Use this for initialization
    void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public bool IsStanding()
    {
        Vector3 rotationEulerAngles = transform.rotation.eulerAngles;

        float rotXPositive = Mathf.Abs(270 - rotationEulerAngles.x);
        float tiltX = rotXPositive < 180f ? rotXPositive : 360 - rotXPositive;

        float rotZPositive = Mathf.Abs(rotationEulerAngles.z);
        float tiltZ = rotZPositive < 180f ? rotZPositive : 360 - rotZPositive;
        
        return standingThreshold > tiltX && standingThreshold > tiltZ;
    }

    public void RaiseIfStanding()
    {
        if (IsStanding())
        {
            transform.Translate(new Vector3(0, distanceToRaise, 0), Space.World);
            _rigidbody.useGravity = false;
        }
    }

    public void Lower()
    {
        transform.rotation = Quaternion.Euler(270f,0f,0f);
        transform.Translate(new Vector3(0, -distanceToRaise, 0), Space.World);
        _rigidbody.useGravity = true;
    }

}
