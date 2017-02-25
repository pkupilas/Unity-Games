using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour
{

    public Text[] frames;
    public Text[] scores;

    // Use this for initialization
    void Start () {
        foreach (var frame in frames)
        {
            frame.text = "1";
        }

        foreach (var score in scores)
        {
            score.text = "111";
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
