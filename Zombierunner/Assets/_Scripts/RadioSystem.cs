using UnityEngine;
using System.Collections;

public class RadioSystem : MonoBehaviour
{

    public AudioClip initialHeliCall;
    public AudioClip innitialCallReply;

    private AudioSource _audioSource;

	// Use this for initialization
	void Start ()
	{
	    _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnMakeInitialHeliCall()
    {
        _audioSource.clip = initialHeliCall;
        _audioSource.Play();
        Invoke("InitialReply", initialHeliCall.length + 1f);
    }

    private void InitialReply()
    {
        _audioSource.clip = innitialCallReply;
        _audioSource.Play();
        BroadcastMessage("OnDispatchHelicopter");
    }
}
