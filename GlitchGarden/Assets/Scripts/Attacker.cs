using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Attacker : MonoBehaviour
{
    [Tooltip("Gap between spawning attacker in seconds.")]
    public float spawnRateSeconds;

    private float currentSpeed;
    private GameObject currentTarget;
    private Animator animator;

    // Use this for initialization
	void Start ()
	{
	    animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
        if (!currentTarget)
        {
            animator.SetBool("isAttacking", false);
        }
	}

    public void SetSpeed(float speed)
    {
        currentSpeed = speed;
    }

    //Used in animation while animation is playing
    public void StrikeCurrentTarget(float damage)
    {
        if (!currentTarget) return;
        var health = currentTarget.GetComponent<Health>();
        if (health)
        {
            health.DealDamage(damage);
        }
    }

    public void Attack(GameObject obj)
    {
        currentTarget = obj;
    }

}
