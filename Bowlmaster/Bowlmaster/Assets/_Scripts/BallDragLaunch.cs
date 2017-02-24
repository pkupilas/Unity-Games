using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Ball))]
public class BallDragLaunch : MonoBehaviour
{
    private Ball _ball;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _startTime;
    private float _endTime;

    // Use this for initialization
    void Start ()
	{
	    _ball = GetComponent<Ball>();
	}

    public void DragStart()
    {
        _startPosition = Input.mousePosition;
        _startTime = Time.time;
    }
    public void DragEnd()
    {
        _endPosition = Input.mousePosition;
        _endTime = Time.time;

        float dragDuration = _endTime - _startTime;
        float launchSpeedX = (_endPosition.x - _startPosition.x) / dragDuration;
        float launchSpeedZ = (_endPosition.y - _startPosition.y) / dragDuration;

        Vector3 velocity = new Vector3(launchSpeedX,0,launchSpeedZ);
        _ball.Launch(velocity);
    }

    public void MoveStart(float delta)
    {
        if (_ball.IsBallInPlay() == false && _ball.IsPositionProperAtStart(delta))
        {
            _ball.transform.Translate(new Vector3(delta, 0, 0));
        }
    }
}
