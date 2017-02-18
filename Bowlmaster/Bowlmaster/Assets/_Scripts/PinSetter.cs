using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour
{
    public int lastStandingCount = 1;
    public Text standingCountText;

    private bool ballEnteredBox = false;
    private float lastChangeTime;
    private Ball ball;

	// Use this for initialization
	void Start ()
	{
	    ball = FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
        standingCountText.text = CountStanding().ToString();

	    if (ballEnteredBox)
	    {
	        CheckStanding();
	    }
	}

    private void CheckStanding()
    {
        int currentStandingPins = CountStanding();
        float settleTime = 3;
        if (currentStandingPins != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = currentStandingPins;
        }
        else if ((Time.time - lastChangeTime) > settleTime)
        {
            PinsHaveSettled();
        }
    }

    private void PinsHaveSettled()
    {
        ball.Reset();
        lastStandingCount = -1;
        ballEnteredBox = false;
        standingCountText.color = Color.green;
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

    public void OnTriggerEnter(Collider coll)
    {
        GameObject objectToHit = coll.gameObject;

        if (objectToHit.GetComponent<Ball>() != null)
        {
            standingCountText.color = Color.red;
            ballEnteredBox = true;
        }
    }

    public void OnTriggerExit(Collider coll)
    {
        GameObject leftObject = coll.gameObject;

        if (leftObject.GetComponent<Pin>() != null)
        {
            Destroy(leftObject);
        }
    }

}
