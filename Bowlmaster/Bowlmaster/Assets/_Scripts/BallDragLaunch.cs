using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Ball))]
public class BallDragLaunch : MonoBehaviour
{
    private Ball ball;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float startTime;
    private float endTime;

    // Use this for initialization
    void Start ()
	{
	    ball = GetComponent<Ball>();
	}

    public void DragStart()
    {
        startPosition = Input.mousePosition;
        startTime = Time.time;
    }
    public void DragEnd()
    {
        endPosition = Input.mousePosition;
        endTime = Time.time;

        float dragDuration = endTime - startTime;
        float launchSpeedX = (endPosition.x - startPosition.x) / dragDuration;
        float launchSpeedZ = (endPosition.y - startPosition.y) / dragDuration;

        Vector3 velocity = new Vector3(launchSpeedX,0,launchSpeedZ);
        ball.Launch(velocity);
    }

    public void MoveStart(float delta)
    {
        //TODO: Clamp ball
        if (ball.inPlay == false)
        {
            ball.transform.Translate(new Vector3(delta, 0, 0));
        }
    }
}
