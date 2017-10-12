using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float health = 250f;
    public float velocity = 5f;
    public GameObject projectile;
    public float projectileVelocity;
    public float fireRate = 0.2f;
    public AudioClip fireSound;

    private float xmin;
    private float xmax;
    private float padding = 1f;

    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        var leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
        var rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distance));
        xmin = leftBoundary.x + padding;
        xmax = rightBoundary.x - padding;
    }

	void Update ()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * velocity * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * velocity * Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, fireRate);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
        

        AdjustPlayerShipXPosition();

    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        Projectile missile = coll.gameObject.GetComponent<Projectile>();
        if (missile != null)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        var levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelManager.LoadLevel("End");
        Destroy(gameObject);
    }


    private void Fire()
    {
        Vector3 offset = transform.position + new Vector3(0, 1f, 0);
        GameObject laserInstance = Instantiate(projectile, offset, Quaternion.identity) as GameObject;
        if (laserInstance != null)
        {
            laserInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileVelocity, 0);
            AudioSource.PlayClipAtPoint(fireSound, transform.position);
        }
    }

    private void AdjustPlayerShipXPosition()
    {
        var clampedX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
