using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WavesManager : MonoBehaviour
{
    public float timeForWave = 60f;
    public Slider slider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        slider.value = Time.timeSinceLevelLoad / timeForWave;
    }
}
