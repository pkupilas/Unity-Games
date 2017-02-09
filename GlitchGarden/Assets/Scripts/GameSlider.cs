using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class GameSlider : MonoBehaviour
{
    public float levelSeconds = 60;

    private Slider slider;
    private GameObject winText;
    private AudioSource audioSource;
    private bool isLevelEnded = false;
    private LevelManager levelManager;

	// Use this for initialization
	void Start ()
	{
	    slider = GetComponent<Slider>(); // possible nullptr exception
	    winText = GameObject.Find("WinPopUp"); // possible nullptr exception
	    audioSource = GetComponent<AudioSource>(); // possible nullptr exception
	    levelManager = FindObjectOfType<LevelManager>(); // possible nullptr exception
        winText.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    slider.value = Time.timeSinceLevelLoad/levelSeconds;
	    bool isTimeUp = Time.timeSinceLevelLoad >= levelSeconds;

        if (isTimeUp && !isLevelEnded)
        {
            HandleWinCondition();
        }

    }

    private void HandleWinCondition()
    {
        DestroyAllTaggedObjects();
        audioSource.Play();
        Invoke("LoadNextLevel", audioSource.clip.length);
        winText.SetActive(true);
        isLevelEnded = true;
    }

    private void DestroyAllTaggedObjects()
    {
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("destroyOnWin");

        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }

    }

    public void LoadNextLevel()
    {
        levelManager.LoadNextLevel();
    }
}
