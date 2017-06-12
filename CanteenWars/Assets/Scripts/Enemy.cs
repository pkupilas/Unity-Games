using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private FoodSpawner _foodSpawner;

    private const float _spawnRate = 5f;
    private const float _maxHealth = 1000;
    private float _currentHealth = _maxHealth;
    private HealthBar _healthBar;


    void Start()
    {
        _healthBar = GetComponent<HealthBar>();
        _healthBar.UpdateHealthBar(_currentHealth, _currentHealth / _maxHealth);
    }

    void Update () {
	    if (Utilities.IsTimeToSpawn(_spawnRate))
	    {
            var spawnedFood = _foodSpawner.SpawnFood(transform.parent.gameObject, _foodSpawner.gameObject).GetComponent<Food>();
            spawnedFood.Throw(GenerateRandomAngle(), GenerateRandomScale());
            _currentHealth += spawnedFood.GetComponent<Food>().Damage;
        }
	}

    private Vector3 GenerateRandomAngle()
    {
        return new Vector3(0f, 0f, Random.Range(30f, 60f));
    }

    private Vector3 GenerateRandomScale()
    {
        return new Vector3(1f, Random.Range(0.6f, 1f), 1f);
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

    public float GetEnemyHealth()
    {
        return _currentHealth;
    }
}
