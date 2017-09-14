using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldObjects.Asteroids;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private AsteroidData _asteroidData;
    private float initialPositionX = -50.5f;
    private float initialPositionY = 50.5f;

    void Start()
    {
        SpawnAsteroids();
    }

    private void SpawnAsteroids()
    {
        for (float i = initialPositionX; i <= 49.5f; i+=1)
        {
            for (float j = initialPositionY; j >= -49.5; j-=1)
            {
                var asteroidPrefab = _asteroidData.AsteroidPrefab;
                Instantiate(asteroidPrefab,new Vector3(i,j,0f),Quaternion.identity);
            }
        }
    }
}
