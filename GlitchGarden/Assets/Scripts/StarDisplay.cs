using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StarDisplay : MonoBehaviour
{
    private Text starText;
    private int stars;
    public enum Status
    {
        SUCCESS,
        FAILURE
    }

	// Use this for initialization
	void Start ()
	{
	    starText = GetComponent<Text>();
	    stars = 100;
        UpdateDisplay();
    }

    public void AddStars(int amount)
    {
        stars += amount;
        UpdateDisplay();
    }


    public Status UseStars(int amount)
    {
        if (stars >= amount)
        {
            stars -= amount;
            UpdateDisplay();
            return Status.SUCCESS;
        }

        return Status.FAILURE;
    }
    private void UpdateDisplay()
    {
        starText.text = stars.ToString();
    }

}
