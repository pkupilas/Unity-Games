using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {
    
    public Text standingCountText;
    
    private int _lastSettledPinsCount = 10;
    private int _lastStandingCount = -1;
    private bool _ballLeftBox = false;
    private float _lastChangeTime;

    private GameManager _gameManager;

    // Use this for initialization
    void Start ()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateStandingCountText();

        if (_ballLeftBox)
        {
            ChangeTextColorToRed();
            UpdateStandingPinsCountAndSettle();
        }
    }
    private void OnTriggerExit(Collider objCollider)
    {
        if (objCollider.gameObject.GetComponent<Ball>() != null)
        {
            SetBallAsLeftTheBox();
        }
    }

    private void UpdateStandingPinsCountAndSettle()
    {
        int currentStandingPins = CountStandingPins();
        const float settleTime = 3;

        if (currentStandingPins != _lastStandingCount)
        {
            _lastChangeTime = Time.time;
            _lastStandingCount = currentStandingPins;
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
        int standingPins = CountStandingPins();
        int fallenPins = _lastSettledPinsCount - standingPins;
        _lastSettledPinsCount = standingPins;

        _gameManager.Bowl(fallenPins);

        _lastStandingCount = -1; // tells that pin have settled and ball is not in box
        _ballLeftBox = false;
        standingCountText.color = Color.green;
    }

    private void UpdateStandingCountText()
    {
        standingCountText.text = CountStandingPins().ToString();
    }

    public void ChangeTextColorToRed()
    {
        standingCountText.color = Color.red;
    }

    public void ResetLastSettledPinsCount()
    {
        _lastSettledPinsCount = 10;
    }

    public void SetBallAsLeftTheBox()
    {
        _ballLeftBox = true;
    }
}
