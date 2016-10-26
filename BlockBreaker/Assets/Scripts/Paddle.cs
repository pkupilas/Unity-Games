using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour
{
    private const float PADDLE_X = 0.5f;
    public bool autoPlay = false;
    public float leftLimit = 0.5f;
    public float rightLimit = 15.5f;
    private Ball ball;

    void Start()
    {
        ball = GameObject.FindObjectOfType<Ball>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!autoPlay)
        {
            MoveWithMouse();
        }
        else
        {
            MoveAutomatically();
        }
    }

    private void MoveAutomatically()
    {
        var paddlePos = new Vector3(PADDLE_X, this.transform.position.y, 0f);
        var autoPosition = ball.transform.position;
        paddlePos.x = Mathf.Clamp(autoPosition.x, leftLimit, rightLimit);
        this.transform.position = paddlePos;
    }

    private void MoveWithMouse()
    {
        var paddlePos = new Vector3(PADDLE_X, this.transform.position.y, 0f);
        var mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
        paddlePos.x = Mathf.Clamp(mousePosInBlocks, leftLimit, rightLimit);
        this.transform.position = paddlePos;
    }
}
