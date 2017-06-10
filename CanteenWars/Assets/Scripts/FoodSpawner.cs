using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{

    [SerializeField] private Food[] _food;


    public GameObject SpawnFood(GameObject spawnPoint, GameObject parent)
    {
        int randomIndex = Random.Range(0, _food.Length);
        var spawnedFood = Instantiate(_food[randomIndex], parent.transform);

        spawnedFood.transform.localPosition =
            new Vector3(spawnPoint.transform.localPosition.x,
                        spawnPoint.transform.localPosition.y,
                        spawnPoint.transform.localPosition.z);

        return spawnedFood.gameObject;
    }
}
