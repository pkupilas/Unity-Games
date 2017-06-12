using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPlatform : MonoBehaviour
{

    [SerializeField] private GameObject[] _movingPoints;
    [SerializeField] private Food[] _food;

    private List<Food> _foodOnPlatform;
    private FoodSpawner _foodSpawner;
    private readonly Vector2 _direction = Vector2.right;
    private const float _moveAcceleration = 100;
    private const float _cooldownTime = 2f;
    private float _timeLeft = _cooldownTime;


    void Start () {
        _foodOnPlatform = new List<Food>();
        _foodSpawner = FindObjectOfType<FoodSpawner>();
    }
	
	void Update ()
	{
	    _timeLeft -= Time.deltaTime;

        if (_timeLeft < 0.1) 
        {
            AddNewDishToPlatform();
            _timeLeft = _cooldownTime;
        }

        Move();
    }

    private void AddNewDishToPlatform()
    {
        var spawnedFood = _foodSpawner.SpawnFood(_movingPoints[0], transform.parent.gameObject).GetComponent<Food>();

        spawnedFood.MakeStaticFood();
        _foodOnPlatform.Add(spawnedFood);
    }

    private void Move()
    {
        for (int i = _foodOnPlatform.Count - 1; i >= 0; i--)
        {
            const float minimalDistance = 0.1f;
            if (Mathf.Abs(_foodOnPlatform[i].transform.position.x - _movingPoints[1].transform.position.x) < minimalDistance)
            {
                var foodToRemove = _foodOnPlatform[i];
                RemoveDishFromPlatform(_foodOnPlatform[i].gameObject);
                Destroy(foodToRemove.gameObject);
            }
            else
            {
                _foodOnPlatform[i].GetComponent<Rigidbody2D>().velocity = _direction * Time.deltaTime * _moveAcceleration;
            }
        }
    }

    public void RemoveDishFromPlatform(GameObject dish)
    {
        dish.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _foodOnPlatform.Remove(dish.GetComponent<Food>());

    }

}
