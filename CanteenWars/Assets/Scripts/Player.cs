using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Arrow _arrow;
    [SerializeField] private GameObject _foodChecker;
    private FoodPlatform _foodPlatform;
    private GameObject _currentFood;
    private GameObject _possibleFood;

    private Vector3 relaseArrowAngle;
    private Vector3 relaseArrowScale;

    
    void Start ()
    {
        _foodPlatform = FindObjectOfType<FoodPlatform>();
        _arrow.TurnOffArrow();
    }
	
	void Update ()
	{
        _possibleFood = CheckForFood();

	    if (!_arrow.isOn)
	    {
            if (Input.GetKey(KeyCode.Space) && _possibleFood != null && _currentFood==null)
            {
                TakeFood(_possibleFood);
            }
            if (_currentFood != null)
            {
                _arrow.TurnOnArrow();
            }
        }
        else 
        {
            if (_arrow.IsRotating() && Input.GetKeyDown(KeyCode.Space))
            {
                relaseArrowAngle = _arrow.StopRotating();
            }
            if (_arrow.IsCruving() && Input.GetKeyUp(KeyCode.Space))
            {
                relaseArrowScale = _arrow.StopCurve();
                _currentFood.GetComponent<Food>().MakeDynamicFood();
                _currentFood.GetComponent<Food>().Throw(relaseArrowAngle, relaseArrowScale);
                _currentFood = null;
                _arrow.TurnOffArrow();
            }
        }
    }

    private void TakeFood(GameObject possibleFood)
    {
        _foodPlatform.RemoveDishFromPlatform(possibleFood);
        _currentFood = possibleFood;
        _currentFood.transform.parent = gameObject.transform.parent;
        _currentFood.transform.localPosition = Vector3.zero;
    }

    private GameObject CheckForFood()
    {
        var linecast = Physics2D.Linecast(transform.position, _foodChecker.transform.position, 1 << LayerMask.NameToLayer("Food"));

        if (linecast)
        {
            return linecast.transform.gameObject;
        }

        return null;
    }
}
