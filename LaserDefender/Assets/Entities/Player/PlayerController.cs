using UnityEngine;

namespace Entities.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _health = 250.0f;
        [SerializeField] private float _velocity = 5.0f;
        [SerializeField] private float _projectileVelocity = 5.0f;
        [SerializeField] private float _fireRate = 0.2f;
        [SerializeField] private AudioClip _fireSound;
        [SerializeField] private GameObject _projectile;

        private float _xmin;
        private float _xmax;
        private const float _padding = 1f;

        void Start()
        {
            float distance = transform.position.z - Camera.main.transform.position.z;
            var leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
            var rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distance));
            _xmin = leftBoundary.x + _padding;
            _xmax = rightBoundary.x - _padding;
        }

        void Update ()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * _velocity * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * _velocity * Time.deltaTime;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                InvokeRepeating("Fire", 0.000001f, _fireRate);
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                CancelInvoke("Fire");
            }

            AdjustPlayerShipXPosition();
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
                    Die();
                }
            }
        }

        private void Die()
        {
            var levelManager = FindObjectOfType<LevelManager>();
            levelManager.LoadLevel("End");
            Destroy(gameObject);
        }
        
        //TODO: Change to coroutine
        private void Fire()
        {
            var offset = transform.position + new Vector3(0, 1f, 0);
            var laserInstance = Instantiate(_projectile, offset, Quaternion.identity);
            if (laserInstance != null)
            {
                laserInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(0, _projectileVelocity, 0);
                AudioSource.PlayClipAtPoint(_fireSound, transform.position);
            }
        }

        private void AdjustPlayerShipXPosition()
        {
            var clampedX = Mathf.Clamp(transform.position.x, _xmin, _xmax);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }
}
