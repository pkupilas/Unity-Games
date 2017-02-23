using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    public Vector3 launchVelocity;
    public bool inPlay;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private Vector3 _startPosition;

	// Use this for initialization
	void Start ()
    {
        inPlay = false;
        _rigidbody = GetComponent<Rigidbody>(); 
        _audioSource = GetComponent<AudioSource>();
        _startPosition = GetComponent<Transform>().position;

        _rigidbody.useGravity = false;
    }

    public void Launch(Vector3 velocity)
    {
        if (velocity != Vector3.zero && !inPlay)
        {
            inPlay = true;
            _rigidbody.useGravity = true;
            _rigidbody.velocity = velocity;
            _audioSource.Play();
        }
    }

    public void Reset()
    {
        inPlay = false;
        transform.rotation = Quaternion.identity;
        transform.position = _startPosition;
        _rigidbody.velocity= Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.useGravity = false;
    }

}
