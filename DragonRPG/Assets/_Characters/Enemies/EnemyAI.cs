using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using _Characters.CommonScripts;
using _Characters.Player;

namespace _Characters.Enemies
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float _chaseRadius = 10f;
        private float _currentWeaponRange;
        private PlayerMovement _player;
        private Character _character;
        private float _distanceToPlayer;
        private enum Status { Idle, Attack, Patrol, Chase}

        private Status _state = Status.Idle;

        void Start()
        {
            _player = FindObjectOfType<PlayerMovement>();
            _character = GetComponent<Character>();
        }

        void Update()
        {
            _distanceToPlayer = Vector3.Distance(_player.transform.position, gameObject.transform.position);
            _currentWeaponRange = GetComponent<WeaponSystem>().CurrentWeaponConfig.MaxAttackRange;

            if (_distanceToPlayer > _chaseRadius && _state != Status.Patrol)
            {
                StopAllCoroutines();
                _state = Status.Patrol;
            }
            if (_distanceToPlayer <= _chaseRadius && _state != Status.Chase)
            {
                StopAllCoroutines();
                _state = Status.Chase;
                StartCoroutine(ChasePlayer());
            }
            if (_distanceToPlayer <= _currentWeaponRange && _state != Status.Attack)
            {
                StopAllCoroutines();
                _state = Status.Attack;
            }
        }

        private IEnumerator ChasePlayer()
        {
            while (_distanceToPlayer >= _currentWeaponRange)
            {
                _character.SetDestination(_player.transform.position);
                yield return new WaitForEndOfFrame();
            }
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