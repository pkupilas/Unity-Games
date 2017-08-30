using UnityEngine;

namespace Characters.Enemies
{
    [CreateAssetMenu(menuName = "Characters/Enemy")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private float _health;
        [SerializeField] private float _speed;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _damage;
        [SerializeField] private float _attackCooldown;

        public float Health => _health;
        public float Speed => _speed;
        public GameObject EnemyPrefab => _enemyPrefab;
        public float Damage => _damage;
        public float AttackCooldown => _attackCooldown;
    }
}