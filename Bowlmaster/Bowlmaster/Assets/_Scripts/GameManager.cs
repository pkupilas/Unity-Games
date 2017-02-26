using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour {

    private List<int> _bowls = new List<int>();
    private PinSetter _pinSetter;
    private Ball _ball;
    private ScoreDisplay _scoreDisplay;

	// Use this for initialization
	void Start ()
	{
	    _pinSetter = FindObjectOfType<PinSetter>();
	    _ball = FindObjectOfType<Ball>();
	    _scoreDisplay = FindObjectOfType<ScoreDisplay>();
	}
	
    public void Bowl(int pinFall)
    {
        _bowls.Add(pinFall);
        var actionToPerform = ActionMaster.NextAction(_bowls);

        _pinSetter.PerformAction(actionToPerform);
        try
        {
            _scoreDisplay.FillRollCard(_bowls);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Exception in FillRollCard().");
        }

        _ball.Reset();
    }
}
