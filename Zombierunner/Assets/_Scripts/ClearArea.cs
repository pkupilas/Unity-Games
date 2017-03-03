using UnityEngine;
using System.Collections;

public class ClearArea : MonoBehaviour
{

    public float _timeSinceLastTrigger = 0f;

    private bool _cleanAreaFounded = false;

	// Update is called once per frame
	void Update ()
	{
	    _timeSinceLastTrigger += Time.deltaTime;
	    if (_timeSinceLastTrigger > 3f && Time.realtimeSinceStartup > 10f && !_cleanAreaFounded)
	    {
	        SendMessageUpwards("OnFindClearArea");
	        _cleanAreaFounded = true;
	    }
	}

    private void OnTriggerStay(Collider coll)
    {
        if (coll.tag != "Player")
        {
            _timeSinceLastTrigger = 0f;
        }
    }
}
