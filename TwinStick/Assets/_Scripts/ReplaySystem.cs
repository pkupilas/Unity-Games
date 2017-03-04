using UnityEngine;
using System.Collections;

public class ReplaySystem : MonoBehaviour
{

    private const int bufferFrames = 100;
    private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];
    private Rigidbody _rigidbody;
    private GameManager _gameManager;
    
	// Use this for initialization
	void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody>();
	    _gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_gameManager.Recording)
        {

            RecordFrames();
        }
        else
        {
            Playback();
        }
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
