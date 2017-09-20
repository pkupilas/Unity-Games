using UnityEngine;
using UnityEngine.AI;
using _Camera;
using _Characters.Enemies;

namespace _Characters.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    [RequireComponent(typeof(CameraRaycaster))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float _stoppingDistance;
        [SerializeField] private float _moveSpeedMultiplier;

        private ThirdPersonCharacter _thirdPersonCharacter;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private Rigidbody _rigidbody;

        private void Start ()
        {
            _thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            SetNavMeshAgent();
            SetCameraRaycaster();
        }

        private void Update()
        {
            _thirdPersonCharacter.Move(
                _navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance
                    ? _navMeshAgent.desiredVelocity
                    : Vector3.zero);
        }

        private void SetNavMeshAgent()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updatePosition = true;
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.stoppingDistance = _stoppingDistance;
        }

        private void SetCameraRaycaster()
        {
            var cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
            cameraRaycaster.onMouseOverTerrain += ProcessMouseOverTerrain;
            cameraRaycaster.onMouseOverEnemy += MoveToEnemy;
        }

        private void ProcessMouseOverTerrain(Vector3 destination)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _navMeshAgent.SetDestination(destination);
            }
        }

        private void MoveToEnemy(Enemy enemy)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(1))
            {
                _navMeshAgent.SetDestination(enemy.transform.position);
            }
        }
        public void OnAnimatorMove()
        {
            // we implement this function to override the default root motion.
            // this allows us to modify the positional speed before it's applied.
            if (Time.deltaTime > 0)
            {
                Vector3 velocity = (_animator.deltaPosition * _moveSpeedMultiplier) / Time.deltaTime;

                // we preserve the existing y part of the current velocity.
                velocity.y = _rigidbody.velocity.y;
                _rigidbody.velocity = velocity;
            }
        }
    }
}
