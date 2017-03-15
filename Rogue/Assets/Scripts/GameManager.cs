using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public float levelStartDelay = 2;
    public float turnDelay = 0.1f;
    public static GameManager instance = null;
    public BoardManager boardScript;
    public int playerFoodPoints = 100;
    [HideInInspector]
    public bool playersTurn = true;

    private Text _levelText;
    private GameObject _levelImage;
    private int level = 0;
    private List<Enemy> _enemies;
    private bool enemiesMoving;
    private bool _doingSetUp;

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
        if (playersTurn || enemiesMoving || _doingSetUp)
        {
            return;
        }

        StartCoroutine(MoveEnemies());
    }

    private void OnLevelWasLoaded(int index)
    {
        level++;
        InitGame();
    }

    public void AddEnemyToList(Enemy script)
    {
        _enemies.Add(script);
    }

    private void InitGame()
    {
        _doingSetUp = true;
        _levelImage = GameObject.Find("LevelImage");
        _levelText = GameObject.Find("LevelText").GetComponent<Text>();
        _levelText.text = "Day: " + level;
        _levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);

        _enemies.Clear();
        boardScript.SetupScene(level);
    }

    private void HideLevelImage()
    {
        _levelImage.SetActive(false);
        _doingSetUp = false;
    }


    // Disabling GameManager if game is over.
    public void GameOver()
    {
        _levelText.text = "You survived: " + level + " days.";
        _levelImage.SetActive(true);
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
