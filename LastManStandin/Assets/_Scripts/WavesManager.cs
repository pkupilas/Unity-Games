using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WavesManager : MonoBehaviour
{
    public float timeForWave = 60f;
    public Slider slider;

    private Player _player;
    private Base _base;
    private LevelManager _levelManager;

	// Use this for initialization
	void Start ()
	{
	    _player = FindObjectOfType<Player>();
	    _base = FindObjectOfType<Base>();
	    _levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateSlider();
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if (_player.health <= 0 || _base.health <=0 || slider.value == slider.maxValue)
        {
            EndGame();
        }

    }

    private void EndGame()
    {
        const int GAME_OVER_SCENE_NUMBER = 2;
        _levelManager.LoadScene(GAME_OVER_SCENE_NUMBER);
    }

    private void UpdateSlider()
    {
        slider.value = Time.timeSinceLevelLoad / timeForWave;
    }

    
}
