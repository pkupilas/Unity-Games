using UnityEngine;

namespace WorldObjects.Asteroids
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField] private AsteroidData _asteroidData;

        private const int AsteroidCount = 10000;
        private float initialPositionX = -49.5f;
        private float initialPositionY = 49.5f;
        private GameObject[] _asteroids;
        private Vector2[] _asteroidsVelocities;
        private Rigidbody2D[] _asteroidsRigidbodies;
        private Collider2D[] _asteroidsColliders;
        private SpriteRenderer[] _asteroidsSpriteRenderers;

        void Start()
        {
            _asteroids = new GameObject[AsteroidCount];
            _asteroidsVelocities = new Vector2[AsteroidCount];
            _asteroidsRigidbodies = new Rigidbody2D[AsteroidCount];
            _asteroidsColliders = new Collider2D[AsteroidCount];
            _asteroidsSpriteRenderers = new SpriteRenderer[AsteroidCount];
            SpawnAsteroids();
            StoreAsteroidsData();
        }

        void Update()
        {
            for (int i = 0; i < AsteroidCount; i++)
            {
                if (_asteroids[i])
                {
                    var tmp = Camera.main.WorldToViewportPoint(_asteroids[i].transform.position);
                    if (0f < tmp.z && 0f < tmp.x && tmp.x < 1f && 0f < tmp.y && tmp.y < 1f)
                    {
                        _asteroidsRigidbodies[i].isKinematic = false;
                        _asteroidsSpriteRenderers[i].enabled = true;
                        _asteroidsColliders[i].enabled = true;
                        _asteroidsRigidbodies[i].velocity = _asteroidsVelocities[i];
                    }
                    else
                    {
                        _asteroidsRigidbodies[i].isKinematic = true;
                        _asteroidsSpriteRenderers[i].enabled = false;
                        _asteroidsColliders[i].enabled = false;
                        _asteroids[i].transform.position += new Vector3(_asteroidsVelocities[i].x, _asteroidsVelocities[i].y,0f) * Time.deltaTime;
                    }
                }
            }
        }

        private void SpawnAsteroids()
        {
            int k = 0;
            for (float i = initialPositionX; i <= 49.5f; i+=1)
            {
                for (float j = initialPositionY; j >= -49.5; j-=1)
                {
                    var newAsteroid = Instantiate(_asteroidData.AsteroidPrefab, new Vector3(i, j, 0f), Quaternion.identity);
                    _asteroids[k] = newAsteroid;
                    k++;
                }
            }
        }

        private void StoreAsteroidsData()
        {
            var rnd = new System.Random(1);
            for (int i = 0; i < AsteroidCount; i++)
            {
                int directionX = rnd.Next(-1, 1);
                float vX = (float)rnd.NextDouble();
                int directionY = rnd.Next(-1, 1);
                float vY = (float)rnd.NextDouble();
                _asteroidsVelocities[i] = new Vector2(directionX * vX * _asteroidData.MaxVelocity, directionY * vY * _asteroidData.MaxVelocity);
                _asteroidsRigidbodies[i] = _asteroids[i].GetComponent<Rigidbody2D>();
                _asteroidsColliders[i] = _asteroids[i].GetComponent<Collider2D>();
                _asteroidsSpriteRenderers[i] = _asteroids[i].GetComponent<SpriteRenderer>();
            }
        }
    }
}
