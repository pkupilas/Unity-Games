using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{

    public float autoLoadTime;

    void Start()
    {
        if (autoLoadTime <= 0)
        {
            Debug.Log("Level auto load disabled.");
        }
        else
        {
            Invoke("LoadNextLevel", autoLoadTime);
        }
    }

    public void LoadLevel(string name)
    {
        Application.LoadLevel(name);
    }

    public void QuitRequest()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

}
