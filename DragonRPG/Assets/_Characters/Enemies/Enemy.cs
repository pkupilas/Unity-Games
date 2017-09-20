using UnityEngine;
using _Characters.Weapons.Projectiles;
using _Core;

namespace _Characters.Enemies
{
    public class Enemy : MonoBehaviour, IDamageable
    {

        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _followRadius = 10f;
        [SerializeField] private float _attackRadius = 5f;
        [SerializeField] private float _attackDamage = 10f;
        [SerializeField] private float _fireRate = 0.5f;

        [SerializeField] private GameObject _projectileToUse;
        [SerializeField] private GameObject _projectileSpawnPoint;
        [SerializeField] private Vector3 _aimOffset = new Vector3(0, 2f, 0);

        private float _currentHealth;
        private bool _isAttacking;

        private Player _player;
        private AICharacterControl _aiCharacterControl;


        void Start()
        {
            _aiCharacterControl = GetComponent<AICharacterControl>();
            _player = FindObjectOfType<Player>();
            _currentHealth = _maxHealth;
        }

        void Update()
        {
            var distanceToPlayer = Vector3.Distance(_player.transform.position, gameObject.transform.position);

            if (distanceToPlayer <= _attackRadius && !_isAttacking)
            {
                _isAttacking = true;
                InvokeRepeating("SpawnProjectile", 0, _fireRate);
            }

            if (distanceToPlayer > _attackRadius)
            {
                _isAttacking = false;
                CancelInvoke("SpawnProjectile");
            }

            _aiCharacterControl.SetTarget(distanceToPlayer <= _followRadius ? _player.transform : transform);
        }
    
        public float HealthAsPercentage
        {
            get { return _currentHealth / _maxHealth; }
        }

        public void TakeDamage(float damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, _maxHealth);
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            // Attack sphere  
            Gizmos.color = new Color(255f, 0f, 0f, 0.5f);
            Gizmos.DrawWireSphere(transform.position, _attackRadius);

            // Follow sphere  
            Gizmos.color = new Color(0f, 0f, 255f, 0.5f);
            Gizmos.DrawWireSphere(transform.position, _followRadius);
        }

        private void SpawnProjectile()
        {
            var newProjectile = Instantiate(_projectileToUse, _projectileSpawnPoint.transform.position, Quaternion.identity);
            var projectileComponent = newProjectile.GetComponent<Projectile>();
            projectileComponent.SetShooter(gameObject);
            var playerFixedPosition = _player.transform.position + _aimOffset;

            var unitVectorToPlayer = (playerFixedPosition - _projectileSpawnPoint.transform.position).normalized;
            projectileComponent.SetDamage(_attackDamage);
            newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileComponent.GetDefaultVelocity();
        }
    }
}