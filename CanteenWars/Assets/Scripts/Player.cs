using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Arrow _arrow;
    private FoodSpawner _foodSpawner;

    private Vector3 relaseArrowAngle;
    private Vector3 relaseArrowScale;

    
    void Start ()
    {
        _foodSpawner = FindObjectOfType<FoodSpawner>();
    }
	
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
            relaseArrowAngle = _arrow.StopRotating();
	    }
        else if (Input.GetKeyUp(KeyCode.Space))
	    {
	        relaseArrowScale = _arrow.StopCurve();

            var spawnedFood = _foodSpawner.SpawnFood(transform.parent.gameObject, _foodSpawner.gameObject).GetComponent<Food>();
            spawnedFood.Throw(relaseArrowAngle, relaseArrowScale);
        }
    }
}
