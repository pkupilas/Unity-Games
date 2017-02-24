using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[RequireComponent(typeof(Animator))]
public class PinSetter : MonoBehaviour
{
    public GameObject pinSet;

    private Animator _animator;
    private PinCounter _pinCounter;

    // Use this for initialization
    void Start ()
	{
	    _animator = GetComponent<Animator>();
	    _pinCounter = FindObjectOfType<PinCounter>();
	}
	
    private void OnTriggerEnter(Collider coll)
    {
        GameObject objectToHit = coll.gameObject;

        if (objectToHit.GetComponent<Ball>() != null)
        {
            _pinCounter.ChangeTextColorToRed();
            _pinCounter.SetBallAsLeftTheBox();
        }
    }

    private void RenewPins()
    {
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

    public void PerformAction(ActionMaster.Action actionToPerform)
    {
        switch (actionToPerform)
        {
            case ActionMaster.Action.Reset:
                _animator.SetTrigger("resetTrigger");
                _pinCounter.ResetLastSettledPinsCount();
                break;
            case ActionMaster.Action.Tidy:
                _animator.SetTrigger("tidyTrigger");
                break;
            case ActionMaster.Action.EndTurn:
                _animator.SetTrigger("resetTrigger");
                _pinCounter.ResetLastSettledPinsCount();
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

}
