using System;
using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	// Use this for initialization
    
    static MusicPlayer instance;
    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource music;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(instance);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
        }
    }

    void OnLevelWasLoaded(int level)
    {
        music.Stop();
        if (level == 0)
        {
            music.clip = startClip;
        }
        else if (level == 1)
        {
            music.clip = gameClip;
        }
        else if (level == 2)
        {
            music.clip = endClip;
        }
        music.loop = true;
        music.Play();
    }


}
