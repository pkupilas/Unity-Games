using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{

    public int wallDamange = 1;
    public int pointsForFood = 10;
    public int pointsForSoda = 20;
    public float restartLevelDelay = 1f;

    private Animator _animator;
    private int food;


    // Use this for initialization
    protected override void Start ()
    {
        _animator = GetComponent<Animator>();
        food = GameManager.instance.playerFoodPoints;
        base.Start();
    }

    private void OnDisable()
    {
        GameManager.instance.playerFoodPoints = food;
    }

	// Update is called once per frame
	void Update ()
	{
	    if (!GameManager.instance.playersTurn) return;
	    int horizontal = 0;
	    int vertical = 0;
	    horizontal = (int) Input.GetAxisRaw("Horizontal");
	    vertical = (int) Input.GetAxisRaw("Vertical");
	    if (horizontal != 0)
	    {
	        vertical = 0;
	    }

	    if (horizontal != 0 || vertical != 0)
	    {
	        AttemptMove<Wall>(horizontal, vertical);
	    }
	}

    private void OnTriggerEnter2d(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
        }
        else if (other.tag == "Food")
        {
            food += pointsForFood;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Soda")
        {
            food += pointsForSoda;
            other.gameObject.SetActive(false);
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamange);
        _animator.SetTrigger("playerChop");
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        food--;
        base.AttemptMove<T>(xDir, yDir);
        RaycastHit2D hit;
        CheckIfGameOver();
        GameManager.instance.playersTurn = false;
    }

    private void CheckIfGameOver()
    {
        if (food <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

    public void LoseFood(int loss)
    {
        _animator.SetTrigger("playerhit");
        food -= loss;
        CheckIfGameOver();
    }
}
