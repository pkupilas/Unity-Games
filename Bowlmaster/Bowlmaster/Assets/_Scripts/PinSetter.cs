using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[RequireComponent(typeof(Animator))]
public class PinSetter : MonoBehaviour
{
    public Text standingCountText;
    public GameObject pinSet;

    private int lastSettledPinsCount = 10;
    private int lastStandingCount = -1;
    private bool _ballLeftBox = false;
    private float _lastChangeTime;

    private Ball _ball;
    private ActionMaster _actionMaster = new ActionMaster(); // here due to only 1 instance
    private Animator _animator;

	// Use this for initialization
	void Start ()
	{
	    _ball = FindObjectOfType<Ball>();
	    _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateStandingCountText();

        if (_ballLeftBox)
        {
            ChangeTextColorToRed();
            UpdateStandingPinsCountAndSettle();
        }
    }

    private void UpdateStandingCountText()
    {
        standingCountText.text = CountStandingPins().ToString();
    }

    private void ChangeTextColorToRed()
    {
        standingCountText.color = Color.red;
    }

    private void UpdateStandingPinsCountAndSettle()
    {
        int currentStandingPins = CountStandingPins();
        const float settleTime = 3;

        if (currentStandingPins != lastStandingCount)
        {
            _lastChangeTime = Time.time;
            lastStandingCount = currentStandingPins;
        }
        else if (Time.time - _lastChangeTime > settleTime)
        {
            PinsHaveSettled();
        }
    }

    private int CountStandingPins()
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

    private void PinsHaveSettled()
    {
        TriggerProperAction();
        _ball.Reset();
        lastStandingCount = -1;
        _ballLeftBox = false;
        standingCountText.color = Color.green;
    }

    private void TriggerProperAction()
    {
        int standinPins = CountStandingPins();
        int fallenPins = lastSettledPinsCount - standinPins;
        var actionToPerform = _actionMaster.Bowl(fallenPins);
        //Debug.Log("Fallen pins: " + fallenPins + ", Action to perform: " + actionToPerform);

        switch (actionToPerform)
        {
            case ActionMaster.Action.Reset:
                _animator.SetTrigger("resetTrigger");
                lastSettledPinsCount = 10;
                break;
            case ActionMaster.Action.Tidy:
                _animator.SetTrigger("tidyTrigger");
                break;
            case ActionMaster.Action.EndTurn:
                _animator.SetTrigger("resetTrigger");
                lastSettledPinsCount = 10;
                break;
            case ActionMaster.Action.EndGame:
                //TODO: Implement endgame
                new UnityException("Not handling atm.");
                break;
            default:
                new UnityException("Invalid action exception.");
                break;
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        GameObject objectToHit = coll.gameObject;

        if (objectToHit.GetComponent<Ball>() != null)
        {
            standingCountText.color = Color.red;
            _ballLeftBox = true;
        }
    }

    private void RenewPins()
    {
        // TODO: Check other form of Instantiate
        GameObject pins = Instantiate(pinSet);
        pins.transform.position = new Vector3(0,10,1829);
    }

    private void RaisePins()
    {
        var pins = FindObjectsOfType<Pin>();

        foreach (var pin in pins)
        {
            pin.RaiseIfStanding();
        }
    }

    private void LowerPins()
    {
        var pins = FindObjectsOfType<Pin>();

        foreach (var pin in pins)
        {
            pin.Lower();
        }
    }

    public void BallLeftTheBox()
    {
        _ballLeftBox = true;
    }

}
