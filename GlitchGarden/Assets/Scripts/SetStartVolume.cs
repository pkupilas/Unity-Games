using UnityEngine;
using System.Collections;

public class SetStartVolume : MonoBehaviour
{

    private MusicManager _musicManager;

	// Use this for initialization
	void Start ()
	{
	    _musicManager = GameObject.FindObjectOfType<MusicManager>();
	    if (_musicManager)
	    {
	        float volume = PlayerPrefsManager.GetMasterVolume();
            _musicManager.SetVolume(volume);
	    }
	    else
	    {
	        Debug.LogWarning("Music manager not found.");
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
