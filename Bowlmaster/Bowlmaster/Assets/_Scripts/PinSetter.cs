using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour
{
    public int lastStandingCount = -1;
    public Text standingCountText;
    public GameObject pinSet;

    private bool _ballEnteredBox = false;
    private float _lastChangeTime;
    private Ball _ball;
    private ActionMaster _actionMaster = new ActionMaster();
    private Animator _animator;
    private int lastSettledPinsCount = 10;

	// Use this for initialization
	void Start ()
	{
	    _ball = FindObjectOfType<Ball>();
	    _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        standingCountText.text = CountStandingPins().ToString();

	    if (_ballEnteredBox)
	    {
	        UpdateStandingPinsCountAndSettle();

	    }
	}

    private void UpdateStandingPinsCountAndSettle()
    {
        int currentStandingPins = CountStandingPins();
        float settleTime = 3;
        if (currentStandingPins != lastStandingCount)
        {
            _lastChangeTime = Time.time;
            lastStandingCount = currentStandingPins;
        }
        else if ((Time.time - _lastChangeTime) > settleTime)
        {
            PinsHaveSettled();
        }
    }

    private void PinsHaveSettled()
    {
        TriggerProperAction();
        _ball.Reset();
        lastStandingCount = -1;
        _ballEnteredBox = false;
        standingCountText.color = Color.green;
    }

    public int CountStandingPins()
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
            _ballEnteredBox = true;
        }
    }

    public void RenewPins()
    {
        // TODO: Check other form of Instantiate
        GameObject pins = Instantiate(pinSet);
        pins.transform.position = new Vector3(0,10,1829);
    }

    public void RaisePins()
    {
        var pins = FindObjectsOfType<Pin>();

        foreach (var pin in pins)
        {
            pin.RaiseIfStanding();
        }
    }

    public void LowerPins()
    {
        var pins = FindObjectsOfType<Pin>();

        foreach (var pin in pins)
        {
            pin.Lower();
        }
    }

    public void TriggerProperAction()
    {
        int standinPins = CountStandingPins();
        int fallenPins = lastSettledPinsCount - standinPins;
        var actionToPerform = _actionMaster.Bowl(fallenPins);
        Debug.Log("Fallen pins: " + fallenPins + ", Action to perform: " + actionToPerform);

        if (actionToPerform == ActionMaster.Action.Reset)
        {
            _animator.SetTrigger("resetTrigger");
            lastSettledPinsCount = 10;
        }
        else if (actionToPerform == ActionMaster.Action.Tidy)
        {
            _animator.SetTrigger("tidyTrigger");
        }
        else if (actionToPerform == ActionMaster.Action.EndTurn)
        {
            _animator.SetTrigger("resetTrigger");
            lastSettledPinsCount = 10;
        }
        else if (actionToPerform == ActionMaster.Action.EndGame)
        {
            //TODO: Implement endgame
            new UnityException("Not handling atm.");
        }
    }
}
