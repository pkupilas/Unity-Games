using UnityEngine;
using System.Collections;

public class ClearArea : MonoBehaviour
{

    private float _timeSinceLastTrigger = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _timeSinceLastTrigger += Time.deltaTime;
	    if (_timeSinceLastTrigger > 1f)
	    {
	        Debug.Log("Clear area!");
	    }
	}

    private void OnTriggerStay(Collider coll)
    {
        _timeSinceLastTrigger = 0f;
    }
}
