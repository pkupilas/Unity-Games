using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScript;

    private int level = 3;

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
	    boardScript = GetComponent<BoardManager>();
	    InitGame();
	}

    private void InitGame()
    {

        boardScript.SetupScene(level);
    }

}
