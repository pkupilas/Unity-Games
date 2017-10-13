using Entities.Player;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField] private float _enemyLaserVelocity = 1f;
        [SerializeField] private float _health = 150f;
        [SerializeField] private float _shotsPerSecond = 0.5f;
        [SerializeField] private int _enemyValue = 100;
        [SerializeField] private GameObject _enemyLaser;
        [SerializeField] private AudioClip _enemyFireSound;
        [SerializeField] private AudioClip _enemyDeathSound;

        private ScoreKeeper scoreKeeper;

        void Start()
        {
            scoreKeeper = FindObjectOfType<ScoreKeeper>();
        }

        void Update()
        {
            float probability = Time.deltaTime * _shotsPerSecond;
            if (Random.value < probability)
            {
                Fire();
            }
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
            var missile = coll.gameObject.GetComponent<Projectile>();
            if (missile != null)
            {
                _health -= missile.Damage;
                missile.Hit();
                if (_health <= 0)
                {
                    if (scoreKeeper != null)
                    {
                        scoreKeeper.Score(_enemyValue);
                    }
                    AudioSource.PlayClipAtPoint(_enemyDeathSound, transform.position);
                    Destroy(gameObject);
                }
            }
        }

        private void Fire()
        {
            Vector3 startPosition = transform.position - new Vector3(0, 1f, 0);
            var enemyLaserInstance = Instantiate(_enemyLaser, startPosition, Quaternion.identity);
            if (enemyLaserInstance)
            {
                AudioSource.PlayClipAtPoint(_enemyFireSound, transform.position);
                enemyLaserInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -_enemyLaserVelocity, 0);
            }
        }
    }
}
