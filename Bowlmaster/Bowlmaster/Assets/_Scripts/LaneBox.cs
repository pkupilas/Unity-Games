using UnityEngine;
using System.Collections;

public class LaneBox : MonoBehaviour
{

    private PinSetter _pinSetter;
	// Use this for initialization
	void Start ()
	{
	    _pinSetter = FindObjectOfType<PinSetter>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerExit(Collider objCollider)
    {
        if (objCollider.gameObject.GetComponent<Ball>() != null)
        {
            _pinSetter.BallLeftTheBox();
            Debug.Log("ball left the LaneBox");
        }

    }
}
