using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{

    public float speed = 5f;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

	// Use this for initialization
	void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        Launch();
    }

    public void Launch()
    {
        _rigidbody.velocity = new Vector3(0, 0, speed);
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
