using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour
{

    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    MoveBullet();
	}

    public void MoveBullet()
    {
        Debug.Log("I AM MOVING");
        float x = 0.1f;
        transform.position += new Vector3(x,0,0);
        //transform.rotation = Quaternion.Euler(x,0,0);
        //var playerPosition = playerTransform.position;

        //if ((transform.position.x != playerPosition.y) && (transform.position.y != playerPosition.y))
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2((playerTransform.rotation.y - transform.position.y), (playerTransform.rotation.x - transform.position.x)) * Mathf.Rad2Deg - 90), 2.0f * Time.deltaTime);
        //}
    }



}
