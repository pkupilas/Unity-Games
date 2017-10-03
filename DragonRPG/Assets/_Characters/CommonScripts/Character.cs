using UnityEngine;
using UnityEngine.AI;

namespace _Characters.CommonScripts
{
    public class Character : MonoBehaviour
    {
        [Header("Animator")]
        [SerializeField] private RuntimeAnimatorController _runtimeAnimatorController;
        [SerializeField] private AnimatorOverrideController _animatorOverrideController;
        [SerializeField] private Avatar _avatar;

        [Header("Capsule Collider")]
        [SerializeField] private Vector3 _colliderCenter = new Vector3(0f, 1f, 0f);
        [SerializeField] private float _colliderRadius = 0.2f;
        [SerializeField] private float _colliderHeight = 2f;

        [Header("Audio Source")]
        [SerializeField] private float _audioSourceSpatialBlend;
        [SerializeField] private float _audioSourceVolume = 0.1f;

        [Header("Movement")]
        [SerializeField] private float _moveSpeedMultiplier;
        [SerializeField] private float _movingTurnSpeed = 360;
        [SerializeField] private float _stationaryTurnSpeed = 180;
        [SerializeField] private float _moveThreshold = 1f;
        [SerializeField] private float _animationSpeedMultiplier = 1.5f;

        [Header("NavMesh Agent")]
        [SerializeField] private float _navMeshAgentSpeed = 2f;
        [SerializeField] private float _navMeshAgentStoppingDistance = 1f;
        [SerializeField] private float _navMeshObstacleAvoidanceRadius = 0.1f;

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private Rigidbody _rigidbody;
        private AudioSource _audioSource;
        private Health _health;
        private float _turnAmount;
        private float _forwardAmount;

        public AnimatorOverrideController AnimatorOverrideController => _animatorOverrideController;

        private void Awake()
        {
            AddAndSetAnimator();
            AddAndSetCapsuleCollider();
            AddAndSetRigidbody();
            AddAndSetAudioSource();
            AddAndSetNavMeshAgent();
        }

        private void AddAndSetAnimator()
        {
            _animator = gameObject.AddComponent<Animator>();
            _animator.runtimeAnimatorController = _runtimeAnimatorController;
            _animator.avatar = _avatar;
        }

        private void AddAndSetCapsuleCollider()
        {
            var capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
            capsuleCollider.center = _colliderCenter;
            capsuleCollider.radius = _colliderRadius;
            capsuleCollider.height = _colliderHeight;
        }

        private void AddAndSetRigidbody()
        {
            _rigidbody = gameObject.AddComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        private void AddAndSetAudioSource()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.spatialBlend = _audioSourceSpatialBlend;
            _audioSource.volume = _audioSourceVolume;
            _audioSource.playOnAwake = false;
        }

        private void AddAndSetNavMeshAgent()
        {
            _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
            _navMeshAgent.updatePosition = true;
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.speed = _navMeshAgentSpeed;
            _navMeshAgent.stoppingDistance = _navMeshAgentStoppingDistance;
            _navMeshAgent.radius = _navMeshObstacleAvoidanceRadius;
        }

        private void Start ()
        {
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            Move(_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance && _health.IsAlive
                    ? _navMeshAgent.desiredVelocity
                    : Vector3.zero);
        }
        
        private void Move(Vector3 movement)
        {
            SetForwardAndTurn(movement);
            ApplyExtraTurnRotation();
            UpdateAnimator();
        }

        private void SetForwardAndTurn(Vector3 movement)
        {
            // convert the world relative moveInput vector into a local-relative
            // turn amount and forward amount required to head in the desired direction.
            if (movement.magnitude > _moveThreshold)
            {
                movement.Normalize();
            }
            var localMovement = transform.InverseTransformDirection(movement);
            _turnAmount = Mathf.Atan2(localMovement.x, localMovement.z);
            _forwardAmount = localMovement.z;
        }

        private void ApplyExtraTurnRotation()
        {
            // help the character turn faster (this is in addition to root rotation in the animation)
            float turnSpeed = Mathf.Lerp(_stationaryTurnSpeed, _movingTurnSpeed, _forwardAmount);
            transform.Rotate(0, _turnAmount * turnSpeed * Time.deltaTime, 0);
        }

        private void UpdateAnimator()
        {
            _animator.SetFloat("Forward", _forwardAmount, 0.1f, Time.deltaTime);
            _animator.SetFloat("Turn", _turnAmount, 0.1f, Time.deltaTime);
            _animator.speed = _animationSpeedMultiplier;
        }

        public void OnAnimatorMove()
        {
            // we implement this function to override the default root motion.
            // this allows us to modify the positional speed before it's applied.
            if (Time.deltaTime > 0)
            {
                var velocity = (_animator.deltaPosition * _moveSpeedMultiplier) / Time.deltaTime;

                // we preserve the existing y part of the current velocity.
                velocity.y = _rigidbody.velocity.y;
                _rigidbody.velocity = velocity;
            }
        }

        public void SetDestination(Vector3 worldPosition)
        {
            _navMeshAgent.destination = worldPosition;
        }
    }
}
