using UnityEngine;
using System.Collections;

public class Defender : MonoBehaviour
{
    private StarDisplay starDisplay;
    public int starCost = 100;

    private void Start()
    {
        starDisplay = FindObjectOfType<StarDisplay>();
    }

    public void AddStars(int starsAmount)
    {
        starDisplay.AddStars(starsAmount);
    }
}
