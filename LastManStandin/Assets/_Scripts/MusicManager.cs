using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

    private AudioSource _audioSource;

	// Use this for initialization
	void Start () {
	    DontDestroyOnLoad(gameObject);
	    _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnLevelWasLoaded(int level)
    {
        if (_audioSource)
        {
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}
