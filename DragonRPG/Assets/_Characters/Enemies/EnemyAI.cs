using System.Collections;
using UnityEngine;
using _Characters.CommonScripts;
using _Characters.Player;

namespace _Characters.Enemies
{
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(WeaponSystem))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private WeapointContainer _weapointContainer;
        [SerializeField] private float _chaseRadius = 10f;

        private float _currentWeaponRange;
        private float _distanceToPlayer;
        private float _waypointStayTime = 0.5f;
        private float _waypointDistanceTolerance = 2f;
        private int _nextWaypointIndex;

        private PlayerMovement _player;
        private Character _character;

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
                StartCoroutine(Patrol());
            }
            if (_distanceToPlayer <= _chaseRadius && _state != Status.Chase)
            {
                StopAllCoroutines();
                StartCoroutine(ChasePlayer());
            }
            if (_distanceToPlayer <= _currentWeaponRange && _state != Status.Attack)
            {
                StopAllCoroutines();
                _state = Status.Attack;
            }
        }

        private IEnumerator Patrol()
        {
            _state = Status.Patrol;
            while (true)
            {
                var nextWaypointPosition = _weapointContainer.transform.GetChild(_nextWaypointIndex).position;
                _character.SetDestination(nextWaypointPosition);
                SetNextWaypointIndex(nextWaypointPosition);
                yield return new WaitForSeconds(_waypointStayTime);
            }
        }

        private void SetNextWaypointIndex(Vector3 waypointPosition)
        {
            if(Vector3.Distance(waypointPosition,transform.position) > _waypointDistanceTolerance) return;
            
            int waypointNumber = _weapointContainer.transform.childCount;
            _nextWaypointIndex++;
            _nextWaypointIndex %= waypointNumber;
        }

        private IEnumerator ChasePlayer()
        {
            _state = Status.Chase;
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