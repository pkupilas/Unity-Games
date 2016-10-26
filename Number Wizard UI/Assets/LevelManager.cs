using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public void LoadLevel(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public void QuitRequest()
    {
        Application.Quit();
    }

}
