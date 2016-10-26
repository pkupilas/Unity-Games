using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject enemyLaser;
    public float enemyLaserVelocity = 1f;
    public float health = 150f;
    public float shotsPerSecond = 0.5f;
    public int enemyValue = 100;
    public AudioClip enemyFireSound;
    public AudioClip enemyDeathSound;

    private ScoreKeeper scoreKeeper;

    void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
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
                if (scoreKeeper != null)
                {
                    scoreKeeper.Score(enemyValue);
                }
                AudioSource.PlayClipAtPoint(enemyDeathSound, transform.position);
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        float probability = Time.deltaTime*shotsPerSecond;
        if (Random.value < probability)
        {
            Fire();
        }
    }

    private void Fire()
    {
        Vector3 startPosition = transform.position - new Vector3(0, 1f, 0);
        GameObject enemyLaserInstance = Instantiate(enemyLaser, startPosition, Quaternion.identity) as GameObject;
        if (enemyLaserInstance)
        {
            AudioSource.PlayClipAtPoint(enemyFireSound, transform.position);
            enemyLaserInstance.rigidbody2D.velocity = new Vector3(0, -enemyLaserVelocity, 0);
        }
    }
}
