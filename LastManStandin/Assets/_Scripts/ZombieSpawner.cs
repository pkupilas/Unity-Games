using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{

    public Zombie zombie;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (IsTimeToSpawn())
	    {
	        SpawnZombie();
	    }
	}

    private bool IsTimeToSpawn()
    {
        float spawnPerSecond = 1 / zombie.spawnRate;

        return Random.value < Time.deltaTime * spawnPerSecond;
    }

    private void SpawnZombie()
    {
        //TODO: Fix to spawn across the game border
        float x = Random.Range(0f, 900f);
        float y = Random.Range(0f, 500f);

        Zombie spawnedZombie = Instantiate(zombie);
        spawnedZombie.transform.position = new Vector3(x, y, 0f);
        spawnedZombie.transform.parent = FindObjectOfType<ZombieSpawner>().transform;
    }

}
