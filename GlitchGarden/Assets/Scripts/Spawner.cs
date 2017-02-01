using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject[] attackers;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
	{
	    foreach (var thisAttacker in attackers)
	    {
	        if (IsTimeToSpawn(thisAttacker))
	        {
                Spawn(thisAttacker);
            }
        }
	}

    private bool IsTimeToSpawn(GameObject thisAttacker)
    {
        var attacker = thisAttacker.GetComponent<Attacker>();
        float spawnDelay = attacker.spawnRateSeconds;
        float spawnPerSecond = 1/spawnDelay;
        const int NUMBER_OF_LINES = 5;

        if (Time.deltaTime > spawnPerSecond)
        {
            Debug.LogWarning("Spawn rate greater than frame rate.");
        }
        float threshold = spawnPerSecond*Time.deltaTime/NUMBER_OF_LINES;

        return Random.value < threshold;
    }

    private void Spawn(GameObject myGameObject)
    {
        var spawnedObject = Instantiate(myGameObject,transform.position, Quaternion.identity ) as GameObject;
        if (spawnedObject == null) return;
        spawnedObject.transform.parent = transform;
    }
}
