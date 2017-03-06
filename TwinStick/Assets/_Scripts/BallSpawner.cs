using UnityEngine;
using System.Collections;

public class BallSpawner : MonoBehaviour
{

    public GameObject ball;

	// Use this for initialization
	void Start () {
        SpawnBall();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    //float threshold = Time.deltaTime;
     //   Debug.Log(threshold);
     //   if (Random.value < threshold)
     //   {
     //       SpawnBall();

     //   }
	}

    private void SpawnBall()
    {
        var ballObject = Instantiate(ball);
        ballObject.transform.position = transform.position;
        //ballObject.GetComponent<Rigidbody>().velocity = new Vector3(100,0,0);
    }

}
