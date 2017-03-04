using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    public bool Recording { get; private set; }

    private float initialFixedDeltaTime;

	// Use this for initialization
	void Start ()
	{
	    Recording = true;
	    initialFixedDeltaTime = Time.fixedDeltaTime;

	}
	
	// Update is called once per frame
	void Update ()
	{
	    ManagePlayback();
	    CheckIfPause();
	}

    private void CheckIfPause()
    {
        if (CrossPlatformInputManager.GetButtonDown("PauseButton"))
        {
            Time.timeScale = 0f;
            Time.fixedDeltaTime = 0f;
        }
        if (CrossPlatformInputManager.GetButtonUp("PauseButton"))
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = initialFixedDeltaTime;
        }
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
