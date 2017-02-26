using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ScoreDisplay : MonoBehaviour
{

    public Text[] rollsText;
    public Text[] frameText;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FillRolls(List<int> rolls)
    {
        for (int i = 0; i < rolls.Count; i++)
        {
            rollsText[i].text = rolls[i].ToString();
        }
    }

    public void FillFrames(List<int> scores)
    {
        for (int i = 0; i < scores.Count; i++)
        {
            frameText[i].text = scores[i].ToString();
        }
    }

    public static string FormatRolls(List<int> rolls)
    {
        string output = "";


        return output;
    }

}
