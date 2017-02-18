using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour
{

    public Text standingCountText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        standingCountText.text = CountStanding().ToString();
	}

    public int CountStanding()
    {
        var pins = FindObjectsOfType<Pin>();
        int standingPinsCounter = 0;

        foreach (var pin in pins)
        {
            if (pin.IsStanding())
            {
                standingPinsCounter++;
            }
        }
        return standingPinsCounter;
    }

}
