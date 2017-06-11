using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPlatform : MonoBehaviour
{

    [SerializeField] private GameObject[] _movingPoints;
    [SerializeField] private Food[] _food;
    private List<Food> _foodOnPlatform;
    private FoodSpawner _foodSpawner;
    private float _spawnRate = 5;
    private Vector2 _direction = Vector2.right;
    private float _moveAcceleration = 100;

    void Start () {
        _foodOnPlatform = new List<Food>();
        _foodSpawner = FindObjectOfType<FoodSpawner>();
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (_foodOnPlatform.Count < 3 && Utilities.IsTimeToSpawn(_spawnRate))
        {
            AddNewDishToPlatform();
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
            if (Mathf.Abs(_foodOnPlatform[i].transform.position.x - _movingPoints[1].transform.position.x) < 0.1)
            {
                var foodToRemove = _foodOnPlatform[i];
                _foodOnPlatform.Remove(_foodOnPlatform[i]);
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
