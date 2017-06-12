using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "Intro")
	    {
	        GameManager.ChangeScene("Main");
	    }
	    else if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "Win" || SceneManager.GetActiveScene().name == "Lose")
        {
            GameManager.ChangeScene("Intro");
        }
	}
}
