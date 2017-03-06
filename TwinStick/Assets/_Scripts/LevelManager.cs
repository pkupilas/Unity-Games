using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private static int levelIndex = 0;
	// Use this for initialization
	void Start () {
	    DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadNextLevel()
    {
        levelIndex++;
        SceneManager.LoadScene(levelIndex);
    }
}
