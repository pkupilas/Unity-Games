using UnityEngine;
using System.Collections;

public class InnerVoice : MonoBehaviour
{

    public AudioClip soundStory;
    public AudioClip goodLandingArea;

    private AudioSource _audioSource;

	// Use this for initialization
	void Start ()
	{
	    _audioSource = GetComponent<AudioSource>();
	    _audioSource.clip = soundStory;
        _audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    private void OnFindClearArea()
    {
        _audioSource.clip = goodLandingArea;
        _audioSource.Play();
        Invoke("CallHeli", goodLandingArea.length + 1f);
    }

    private void CallHeli()
    {
        SendMessageUpwards("OnMakeInitialHeliCall");
    }
}
