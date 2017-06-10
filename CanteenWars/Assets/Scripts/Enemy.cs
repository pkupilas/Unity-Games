using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private FoodSpawner _foodSpawner;
    private float _spawnRate = 5f;
	

	void Update () {
	    if (IsTimeToSpawn())
	    {
            var spawnedFood = _foodSpawner.SpawnFood(transform.parent.gameObject).GetComponent<Food>();
            spawnedFood.Throw(GenerateRandomAngle(), GenerateRandomScale());
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

    private bool IsTimeToSpawn()
    {
        float spawnPerSecond = 1 / _spawnRate;

        return Random.value < Time.deltaTime * spawnPerSecond;
    }
}
