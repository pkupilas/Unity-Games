using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{

    [SerializeField] private Food[] _food;
    [SerializeField] private GameObject _comboOnEnemySpawn;
    [SerializeField] private GameObject _comboOnPlayerSpawn;
    [SerializeField] private Food _comboFood;

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

    public GameObject SpawnComboShot(string target)
    {
        Debug.Log("SPECIAL COMBO");
        GameObject comboParent = (target == "Enemy")
            ? _comboOnEnemySpawn
            : _comboOnPlayerSpawn;
    
        var spawnedFood = Instantiate(_comboFood, comboParent.transform);

        spawnedFood.transform.localPosition = Vector3.zero;
        spawnedFood.transform.localScale = new Vector3(5,5,1);
        return spawnedFood.gameObject;
    }
}
