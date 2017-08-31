using UnityEngine;

namespace Characters.Enemies
{
    [CreateAssetMenu(menuName = "Characters/Enemy")]
    public class EnemyData : CharacterData
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _damage;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _attackRadius = 5f;

        public GameObject EnemyPrefab => _enemyPrefab;
        public float Damage => _damage;
        public float AttackCooldown => _attackCooldown;
        public float AttackRadius => _attackRadius;
    }
}