using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour
{

    public float spawnRate = 5;
    public float speed = 2;

    private Player _player;
    // Use this for initialization
	void Start ()
	{
	    _player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    HandleMoving();
	}

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Ammunition>() != null)
        {
            Destroy(coll.gameObject);
            Destroy(gameObject);
        }
    }

    private void HandleMoving()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, step);
    }
}
