using UnityEngine;

namespace Characters.Enemies
{
    [CreateAssetMenu(menuName = "Characters/Enemy")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private float _health;
        [SerializeField] private float _speed;
        [SerializeField] private GameObject _enemyPrefab;

        public float Health => _health;
        public float Speed => _speed;
        public GameObject EnemyPrefab => _enemyPrefab;
    }
}