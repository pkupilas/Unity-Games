using UnityEngine;
using _Characters.CommonScripts;
using _Characters.Weapons.Projectiles;

namespace _Characters.Enemies
{
    public class Enemy : MonoBehaviour
    {

        [SerializeField] private float _followRadius = 10f;
        [SerializeField] private float _attackRadius = 5f;
        [SerializeField] private float _attackDamage = 10f;
        [SerializeField] private float _fireRate = 0.5f;

        [SerializeField] private GameObject _projectileToUse;
        [SerializeField] private GameObject _projectileSpawnPoint;
        [SerializeField] private Vector3 _aimOffset = new Vector3(0, 2f, 0);
        
        private bool _isAttacking;
        private Player.Player _player;
        private Health _health;
        
        void Start()
        {
            _player = FindObjectOfType<Player.Player>();
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

            //_aiCharacterControl.SetTarget(distanceToPlayer <= _followRadius ? _player.transform : transform);
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

        // TODO: Change to Coroutine
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