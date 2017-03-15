using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource effectsSource;
    public AudioSource musicSource;
    public static SoundManager instance = null;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;



	// Use this for initialization
	void Awake () {
	    if (instance == null)
	    {
	        instance = this;
	    }
	    else if (instance != null) 
	    {
	        Destroy(gameObject);
	    }

        DontDestroyOnLoad(gameObject);
	}

    public void PlaySingle(AudioClip clip)
    {
        effectsSource.clip = clip;
        effectsSource.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        effectsSource.pitch = randomPitch;
        effectsSource.clip = clips[randomIndex];
        effectsSource.Play();
    }

}
