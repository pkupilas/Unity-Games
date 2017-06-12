using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Arrow _arrow;
    [SerializeField] private GameObject[] _foodCheckers;

    private FoodPlatform _foodPlatform;
    private GameObject _currentFood;
    private GameObject _possibleFood;
    private HealthBar _healthBar;

    private Vector3 _relaseArrowAngle;
    private Vector3 _relaseArrowScale;
    private const float _maxHealth = 1000;
    private float _currentHealth = _maxHealth;

    
    void Start ()
    {
        _foodPlatform = FindObjectOfType<FoodPlatform>();
        _arrow.TurnOffArrow();
        _healthBar = GetComponent<HealthBar>();
        _healthBar.UpdateHealthBar(_currentHealth, _currentHealth / _maxHealth);
    }
	
	void Update ()
	{
        _possibleFood = CheckForFood();

	    if (!_arrow.IsTurnedOn)
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
                _relaseArrowAngle = _arrow.StopRotating();
            }
            if (_arrow.IsCruving() && Input.GetKeyUp(KeyCode.Space))
            {
                _relaseArrowScale = _arrow.StopCurve();
                _currentFood.GetComponent<Food>().MakeDynamicFood();
                _currentFood.GetComponent<Food>().Throw(_relaseArrowAngle, _relaseArrowScale);
                _currentFood = null;
                _arrow.TurnOffArrow();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var food = other.GetComponent<Food>();

        if (food != null)
        {
            DealDamage(food.Damage);
        }
    }

    private void DealDamage(float damage)
    {
        _currentHealth -= damage;
        _healthBar.UpdateHealthBar(_currentHealth, _currentHealth / _maxHealth);
    }

    private void TakeFood(GameObject possibleFood)
    {
        _foodPlatform.RemoveDishFromPlatform(possibleFood);
        _currentFood = possibleFood;
        _currentFood.transform.parent = gameObject.transform.parent;
        _currentFood.transform.localPosition = Vector3.zero;
        _currentHealth += _currentFood.GetComponent<Food>().Damage;
    }

    private GameObject CheckForFood()
    {
        var linecast = Physics2D.Linecast(_foodCheckers[0].transform.position, _foodCheckers[1].transform.position, 1 << LayerMask.NameToLayer("Food"));

        if (linecast)
        {
            return linecast.transform.gameObject;
        }

        return null;
    }

    public float GetPlayerHealth()
    {
        return _currentHealth;
    }
}
