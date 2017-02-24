using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    public Vector3 launchVelocity;

    private bool inPlay;

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
        TurnOffGravity();
    }

    public void Launch(Vector3 velocity)
    {
        if (velocity != Vector3.zero && !inPlay)
        {
            inPlay = true;
            TurnOnGravity();
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
        TurnOffGravity();
    }

    public bool IsBallInPlay()
    {
        return inPlay;
    }

    private void TurnOffGravity()
    {
        _rigidbody.useGravity = false;
    }

    private void TurnOnGravity()
    {
        _rigidbody.useGravity = true;
    }

    public bool IsPositionProperAtStart(float delta)
    {
        const float LEFT_BORDER = -35f;
        const float RIGHT_BORDER = 35f;
        float ballPositionX = transform.position.x;

        return ballPositionX + delta < RIGHT_BORDER && ballPositionX + delta > LEFT_BORDER;
    }
}
