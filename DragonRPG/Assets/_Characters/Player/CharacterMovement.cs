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

        private ThirdPersonCharacter _thirdPersonCharacter;
        private GameObject _walkTarget;
        private NavMeshAgent _navMeshAgent;

        private void Start ()
        {
            _thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
            SetNavMeshAgent();
            SetCameraRaycaster();
            _walkTarget = new GameObject("WalkTarget");
        }

        private void Update()
        {
            _thirdPersonCharacter.Move(
                _navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance
                    ? _navMeshAgent.desiredVelocity
                    : Vector3.zero, false, false);
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
    }
}
