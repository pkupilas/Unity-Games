using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public Vector3 launchVelocity;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

	// Use this for initialization
	void Start ()
    {
        //TODO: Fix possible nulls
        _rigidbody = GetComponent<Rigidbody>(); 
        _audioSource = GetComponent<AudioSource>();

        _rigidbody.useGravity = false;

        //Launch(launchVelocity);
    }

    public void Launch(Vector3 velocity)
    {
        _rigidbody.useGravity = true;
        _rigidbody.velocity = velocity;
        _audioSource.Play();
    }

}
