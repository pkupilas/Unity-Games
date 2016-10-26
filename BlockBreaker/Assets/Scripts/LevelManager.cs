using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public void LoadLevel(string sceneName)
    {
        Application.LoadLevel(sceneName);
        Brick.breakableCount = 0;
    }

    public void QuitRequest()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        Brick.breakableCount = 0;
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void CheckIfAllBricksDestroyed()
    {
        if (Brick.breakableCount <= 0)
        {
            LoadNextLevel();
        }
    }

}
