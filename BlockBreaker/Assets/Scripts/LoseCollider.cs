using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour
{
    public int Lives = 3;

    private LevelManager lvlManger;

    void OnTriggerEnter2D(Collider2D trigger)
    {
        Lives--;
        if (Lives <= 0)
        {
            lvlManger = FindObjectOfType<LevelManager>();
            lvlManger.LoadLevel("Lose");
        }
        else
        {
            ContinueGame();
        }

    }

    private void ContinueGame()
    {
        var ball = FindObjectOfType<Ball>();
        ball.hasStarted = false;
    }
}
