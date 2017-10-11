using UnityEngine;

public class Ball : MonoBehaviour
{
    private Paddle _paddle;
    private Vector3 _paddleToBallVector;

    public bool HasStarted { get; set; }
    
	void Start()
	{
	    _paddle = FindObjectOfType<Paddle>();
	    _paddleToBallVector = transform.position - _paddle.transform.position;
	}
    
    void Update()
    {
        if (HasStarted) return;

        transform.position = _paddle.transform.position + _paddleToBallVector;

        if (Input.GetMouseButtonDown(0))
        {
            HasStarted = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 10f);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (HasStarted)
        {
            Vector2 tweak = new Vector2(Random.Range(0f,0.2f),Random.Range(0f,0.2f));
            GetComponent<Rigidbody2D>().velocity += tweak;
            GetComponent<AudioSource>().Play();
        }
    }
}
