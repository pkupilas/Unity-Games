using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    public bool Recording { get; private set; }

	// Use this for initialization
	void Start ()
	{
	    Recording = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    ManagePlayback();
	}

    void ManagePlayback()
    {
        if (CrossPlatformInputManager.GetButton("Fire4"))
        {
            Recording = false;
        }
        else
        {
            Recording = true;
        }
    }


}
