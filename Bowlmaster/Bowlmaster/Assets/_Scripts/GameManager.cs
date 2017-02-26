using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour {

    private List<int> _rolls = new List<int>();
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
        _rolls.Add(pinFall);
        var actionToPerform = ActionMaster.NextAction(_rolls);

        _pinSetter.PerformAction(actionToPerform);
        _scoreDisplay.FillRolls(_rolls);
        _scoreDisplay.FillFrames(ScoreMaster.ScoreCumulative(_rolls));
        _ball.Reset();
    }
}
