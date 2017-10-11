using UnityEngine;

public class LoseCollider : MonoBehaviour
{
    private int _lives = 3;
    private LevelManager _lvlManger;

    void Start()
    {
        _lvlManger = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        _lives--;
        if (_lives <= 0)
        {
            _lvlManger.LoadLevel("Lose");
        }
        else
        {
            ContinueGame();
        }
    }

    private void ContinueGame()
    {
        var ball = FindObjectOfType<Ball>();
        ball.HasStarted = false;
    }
}
