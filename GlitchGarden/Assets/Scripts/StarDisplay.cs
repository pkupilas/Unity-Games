using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StarDisplay : MonoBehaviour
{
    private Text starText;
    private int stars;

	// Use this for initialization
	void Start ()
	{
	    starText = GetComponent<Text>();
	    stars = 0;
	}

    public void AddStars(int amount)
    {
        stars += amount;
        UpdateDisplay();
    }


    public void UseStars(int amount)
    {
        if (stars > 0)
        {
            stars -= amount;
            UpdateDisplay();
        }
    }
    private void UpdateDisplay()
    {
        starText.text = stars.ToString();
    }

}
