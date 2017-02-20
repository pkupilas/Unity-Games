using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour
{

    public float spawnRate = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Ammunition>() != null)
        {
            Destroy(coll.gameObject);
            Destroy(gameObject);
        }
    }
}
