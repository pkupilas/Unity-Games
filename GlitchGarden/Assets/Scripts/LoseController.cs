using UnityEngine;
using System.Collections;

public class LoseController : MonoBehaviour
{
    public int loseCondition = 3;
    public LevelManager levelManager;

	// Use this for initialization
	void Start ()
	{
	    levelManager = FindObjectOfType<LevelManager>(); // posible nullptr exception
	}
	
    private void OnTriggerEnter2D()
    {
        loseCondition--;
        if (loseCondition <= 0)
        {
            levelManager.LoadLevel("03b Lose");
        }
    }
}
