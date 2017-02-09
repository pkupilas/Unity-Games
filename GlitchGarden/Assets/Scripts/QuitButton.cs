using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour
{

    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnMouseDown()
    {
        levelManager.LoadLevel("01a Start");
    }
}
