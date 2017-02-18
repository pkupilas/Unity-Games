using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour
{

    public float standingThreshold = 3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    Debug.Log(name + " " + IsStanding());
	}

    public bool IsStanding()
    {
        Vector3 rotationEulerAngles = transform.rotation.eulerAngles;

        float rotXPositive = Mathf.Abs(rotationEulerAngles.x);
        float tiltX = rotXPositive < 180f ? rotXPositive : 360 - rotXPositive;

        float rotZPositive = Mathf.Abs(rotationEulerAngles.z);
        float tiltZ = rotZPositive < 180f ? rotZPositive : 360 - rotZPositive;
        
        return standingThreshold > tiltX && standingThreshold > tiltZ;
    }

}
