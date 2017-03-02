using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public AudioClip soundStory;

    private bool reSpawner = false;
    private Transform[] _spawnPoints;
    private Helicopter _helicopter;
    private AudioSource _innerVoice;

	// Use this for initialization
	void Start ()
    {
        _spawnPoints = GameObject.Find("Player Spawn Points").GetComponentsInChildren<Transform>();
        _helicopter = FindObjectOfType<Helicopter>();

        SetAudioSource();
        ReSpawn();
        _innerVoice.clip = soundStory;
        _innerVoice.Play();
    }

    // Update is called once per frame
    void Update () {
	    if (reSpawner)
	    {
	        ReSpawn();
	        reSpawner = false;
	    }
	}

    private void SetAudioSource()
    {
        var audioSources = GetComponents<AudioSource>();
        foreach (var audioSource in audioSources)
        {
            const int innerVoiceValue = 1;
            if (audioSource.priority == innerVoiceValue)
            {
                _innerVoice = audioSource;
            }
        }
    }

    private void ReSpawn()
    {
        int randomIndex = Random.Range(1, _spawnPoints.Length);
        transform.position = _spawnPoints[randomIndex].position;
    }

    private void OnFindClearArea()
    {
        _helicopter.Call();
    }
}
