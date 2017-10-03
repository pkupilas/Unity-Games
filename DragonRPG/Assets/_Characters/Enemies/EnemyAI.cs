using UnityEngine;
using _Characters.CommonScripts;
using _Characters.Player;

namespace _Characters.Enemies
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float _chaseRadius = 10f;
        private float _currentWeaponRange;
        private PlayerMovement _playerMovement;
        
        void Start()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
        }

        void Update()
        {
            var distanceToPlayer = Vector3.Distance(_playerMovement.transform.position, gameObject.transform.position);
            _currentWeaponRange = GetComponent<WeaponSystem>().CurrentWeaponConfig.MaxAttackRange;
        }

        private void OnDrawGizmos()
        {
            // Attack sphere  
            Gizmos.color = new Color(255f, 0f, 0f, 0.5f);
            Gizmos.DrawWireSphere(transform.position, _currentWeaponRange);

            // Follow sphere  
            Gizmos.color = new Color(0f, 0f, 255f, 0.5f);
            Gizmos.DrawWireSphere(transform.position, _chaseRadius);
        }
    }
}