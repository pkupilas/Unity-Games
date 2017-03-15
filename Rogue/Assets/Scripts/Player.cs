using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingObject
{

    public int wallDamange = 1;
    public int pointsForFood = 10;
    public int pointsForSoda = 20;
    public float restartLevelDelay = 1f;
    public Text foodText;
    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip eatSound1;
    public AudioClip eatSound2;
    public AudioClip drinkSound1;
    public AudioClip drinkSound2;
    public AudioClip gameOverSound;

    private Animator _animator;
    private int food;


    // Use this for initialization
    protected override void Start ()
    {
        _animator = GetComponent<Animator>();
        food = GameManager.instance.playerFoodPoints;
        foodText.text = "Food: " + food;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Exit"))
        {
            Invoke("Restart", restartLevelDelay);
        }
        else if (other.CompareTag("Food"))
        {
            food += pointsForFood;

            foodText.text = "+ " + pointsForFood + " Food: " + food;

            SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Soda"))
        {
            food += pointsForSoda;
            foodText.text = "+ " + pointsForSoda + " Food: " + food;
            SoundManager.instance.RandomizeSfx(drinkSound1, drinkSound2);
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
        foodText.text = "Score: " + food;
        base.AttemptMove<T>(xDir, yDir);

        RaycastHit2D hit; // allow to reference result in move
        if (Move(xDir, yDir, out hit))
        {
            SoundManager.instance.RandomizeSfx(moveSound1,moveSound2);
        }


        CheckIfGameOver();
        GameManager.instance.playersTurn = false;
    }

    private void CheckIfGameOver()
    {
        if (food <= 0)
        {
            SoundManager.instance.PlaySingle(gameOverSound);
            SoundManager.instance.musicSource.Stop();
            GameManager.instance.GameOver();
        }
    }

    public void LoseFood(int loss)
    {
        _animator.SetTrigger("playerHit");
        food -= loss;
        foodText.text = "- " + loss + " Food: " + food;
        CheckIfGameOver();
    }
}
