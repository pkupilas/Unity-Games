using UnityEngine;
using System.Collections;

public class ClearArea : MonoBehaviour
{

    public float _timeSinceLastTrigger = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _timeSinceLastTrigger += Time.deltaTime;
	    if (_timeSinceLastTrigger > 3f && Time.realtimeSinceStartup > 10f)
	    {
	        SendMessageUpwards("OnFindClearArea");
	    }
	}

    private void OnTriggerStay(Collider coll)
    {
        _timeSinceLastTrigger = 0f;
    }
}
