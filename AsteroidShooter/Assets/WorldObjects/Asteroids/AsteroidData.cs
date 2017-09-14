using UnityEngine;

namespace WorldObjects.Asteroids
{
    [CreateAssetMenu(menuName = "Asteroid")]
    public class AsteroidData : ScriptableObject
    {
        [SerializeField] private GameObject _asteroidPrefab;

        public GameObject AsteroidPrefab => _asteroidPrefab;
    }
}
