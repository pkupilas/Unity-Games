using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public bool reSpawner = false;

    private Transform[] _spawnPoints;

	// Use this for initialization
	void Start ()
	{
	    _spawnPoints = GameObject.Find("Player Spawn Points").GetComponentsInChildren<Transform>();
	    ReSpawn();
	}

    // Update is called once per frame
	void Update () {
	    if (reSpawner)
	    {
	        ReSpawn();
	        reSpawner = false;
	    }
	}

    private void ReSpawn()
    {
        int randomIndex = Random.Range(1, _spawnPoints.Length);
        transform.position = _spawnPoints[randomIndex].position;
    }

}
