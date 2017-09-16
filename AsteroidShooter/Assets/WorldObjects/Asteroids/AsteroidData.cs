using UnityEngine;

namespace WorldObjects.Asteroids
{
    [CreateAssetMenu(menuName = "Asteroid")]
    public class AsteroidData : ScriptableObject
    {
        [SerializeField] private GameObject _asteroidPrefab;
        [SerializeField] private float _maxVelocity = 2;

        public GameObject AsteroidPrefab => _asteroidPrefab;
        public float MaxVelocity => _maxVelocity;
    }
}
