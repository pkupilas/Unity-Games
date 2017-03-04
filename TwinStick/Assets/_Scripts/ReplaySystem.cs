using UnityEngine;
using System.Collections;

public class ReplaySystem : MonoBehaviour
{

    private const int bufferFrames = 100;
    private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];
    private Rigidbody _rigidbody;
    
	// Use this for initialization
	void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        RecordFrames();
    }

    private void RecordFrames()
    {
        _rigidbody.isKinematic = false;
        int frame = Time.frameCount % bufferFrames;
        keyFrames[frame] = new MyKeyFrame(Time.time, transform.position, transform.rotation);
    }

    private void Playback()
    {
        _rigidbody.isKinematic = true;
        int frame = Time.frameCount % bufferFrames;
        transform.position = keyFrames[frame].Pos;
        transform.rotation = keyFrames[frame].Rot;
    }
}
