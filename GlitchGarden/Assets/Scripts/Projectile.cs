using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    transform.Translate(Vector3.right * speed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        var attacker = other.gameObject.GetComponent<Attacker>();
        var health = other.gameObject.GetComponent<Health>();

        if (attacker && health)
        {
            Destroy(gameObject);
            health.DealDamage(damage);
        }
    }
}
