using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public float turnDelay = 0.1f;
    public static GameManager instance = null;
    public BoardManager boardScript;
    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true;

    private int level = 3;
    private List<Enemy> _enemies;
    private bool enemiesMoving;

	// Use this for initialization
	void Awake ()
	{
	    if (instance == null)
	    {
	        instance = this;
	    }
	    else
	    {
	        Destroy(gameObject);
	    }

        DontDestroyOnLoad(gameObject);
        _enemies = new List<Enemy>();
	    boardScript = GetComponent<BoardManager>();
	    InitGame();
	}

    void Update()
    {
        if (playersTurn || enemiesMoving)
        {
            return;
        }

        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy script)
    {
        _enemies.Add(script);
    }

    private void InitGame()
    {
        _enemies.Clear();
        boardScript.SetupScene(level);
    }

    // Disabling GameManager if game is over.
    public void GameOver()
    {
        enabled = false;
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
        if (_enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].MoveEnemy();
            yield return new WaitForSeconds(_enemies[i].moveTime);
        }

        playersTurn = true;
        enemiesMoving = false;
    }

}
