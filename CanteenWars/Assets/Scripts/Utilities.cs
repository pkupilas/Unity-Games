using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour {

    public static bool IsTimeToSpawn(float _spawnRate)
    {
        float spawnPerSecond = 1 / _spawnRate;

        return Random.value < Time.deltaTime * spawnPerSecond;
    }
}
