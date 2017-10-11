using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private bool _autoPlay;
    
    private float _leftBoundary = 0.5f;
    private float _rightBoundary = 15.5f;
    private Ball _ball;

    void Start()
    {
        _ball = FindObjectOfType<Ball>();
    }
	
	void Update ()
    {
        if (_autoPlay)
        {
            MovePaddle(_ball.transform.position.x);
        }
        else
        {
            MovePaddle(Input.mousePosition.x / Screen.width * 16);
        }
    }

    private void MovePaddle(float position)
    {
        var paddlePos = new Vector3(
                Mathf.Clamp(position, _leftBoundary, _rightBoundary),
                transform.position.y,
                0f);
        transform.position = paddlePos;
    }
}
