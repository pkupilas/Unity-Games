using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private Player _player;
    private Enemy _enemy;


    // Use this for initialization
    void Start ()
    {
        _player = FindObjectOfType<Player>();
        _enemy = FindObjectOfType<Enemy>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (_player.GetPlayerHealth() <= 0)
	    {
            ChangeScene("Lose");
        }
        else if (_enemy.GetEnemyHealth() <= 0)
	    {
	        ChangeScene("Win");
	    }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
