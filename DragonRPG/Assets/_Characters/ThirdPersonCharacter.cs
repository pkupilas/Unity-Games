using UnityEngine;

namespace _Characters
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class ThirdPersonCharacter : MonoBehaviour
	{
	    [SerializeField] private float _movingTurnSpeed = 360;
	    [SerializeField] private float _stationaryTurnSpeed = 180;

		private Rigidbody _rigidbody;
	    private Animator _animator;
	    private float _turnAmount;
	    private float _forwardAmount;
        
        void Start()
		{
			_animator = GetComponent<Animator>();
			_rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            _animator.applyRootMotion = true;
        }
        
		public void Move(Vector3 move)
		{
			// convert the world relative moveInput vector into a local-relative
			// turn amount and forward amount required to head in the desired
			// direction.
		    if (move.magnitude > 1f)
		    {
		        move.Normalize();
		    }
			move = transform.InverseTransformDirection(move);
			move = Vector3.ProjectOnPlane(move, Vector3.zero);
			_turnAmount = Mathf.Atan2(move.x, move.z);
			_forwardAmount = move.z;

			ApplyExtraTurnRotation();
			UpdateAnimator(move);
		}

		private void UpdateAnimator(Vector3 move)
		{
			_animator.SetFloat("Forward", _forwardAmount, 0.1f, Time.deltaTime);
			_animator.SetFloat("Turn", _turnAmount, 0.1f, Time.deltaTime);
		}

		private void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(_stationaryTurnSpeed, _movingTurnSpeed, _forwardAmount);
			transform.Rotate(0, _turnAmount * turnSpeed * Time.deltaTime, 0);
		}    
    }
}
