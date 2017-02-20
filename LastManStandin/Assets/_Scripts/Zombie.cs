using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Zombie : MonoBehaviour
{

    public float spawnRate = 5;
    public float speed = 2;
    private Vector3 objectOfInterestPosition;
    private Base _base;
    private Player _player;

    // Use this for initialization
	void Start ()
	{
	    _player = FindObjectOfType<Player>();
	    _base = FindObjectOfType<Base>();
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
        else if (coll.gameObject.GetComponent<Base>() != null)
        {
            StopMoving();
        }
    }

    private void StopMoving()
    {
        speed = 0;
    }

    private void HandleMoving()
    {
        float step = speed * Time.deltaTime;

        UpdateObjectOfInterest();
        transform.position = Vector3.MoveTowards(transform.position, objectOfInterestPosition, step);
    }

    private void UpdateObjectOfInterest()
    {
        float distanceToPlayer = GetVectorLength(_player.transform);
        float distanceToBase = GetVectorLength(_base.transform);

        objectOfInterestPosition = distanceToPlayer > distanceToBase ? _base.transform.position : _player.transform.position;
    }

    private float GetVectorLength(Transform objTransform)
    {
        Vector3 length = objTransform.position - transform.position;

        return length.magnitude;
    }
}
