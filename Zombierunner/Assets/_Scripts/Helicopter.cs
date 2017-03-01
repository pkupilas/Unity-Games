using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour
{

    public AudioClip callHeliSound;

    private bool isCalled = false;
    private AudioSource _audioSource;

    // Use this for initialization
    void Start () {
        _audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        HandleCallHeli();
    }

    private void HandleCallHeli()
    {
        if (Input.GetButton("CallHeli") && !isCalled)
        {
            _audioSource.clip = callHeliSound;
            _audioSource.Play();
            isCalled = true;
        }
    }
}
