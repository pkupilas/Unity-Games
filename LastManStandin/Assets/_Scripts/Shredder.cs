using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Destroying " + coll.name);
        GameObject objectToDestroy = coll.gameObject;
        Destroy(objectToDestroy);
    }
}
