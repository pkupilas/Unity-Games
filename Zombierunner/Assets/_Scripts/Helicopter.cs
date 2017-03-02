using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour
{

    public AudioClip callHeliSound;

    private bool isCalled = false;
    private AudioSource _audioSource;
    private Rigidbody _rigidbody;

    // Use this for initialization
    void Start () {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        Call();
    }

    public void Call()
    {
        if (Input.GetButton("CallHeli") && !isCalled)
        {
            _audioSource.clip = callHeliSound;
            _audioSource.Play();
            isCalled = true;
            AddVelocity();
        }
    }

    private void AddVelocity()
    {
        _rigidbody.velocity = new Vector3(0, 0, 50f);
    }
}
