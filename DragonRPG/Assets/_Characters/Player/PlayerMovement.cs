using UnityEngine;
using UnityEngine.AI;
using _Camera;
using _Characters.Enemies;

// TODO: Consider rewiring


namespace _Characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AICharacterControl))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    [RequireComponent(typeof(CameraRaycaster))]
    public class PlayerMovement : MonoBehaviour
    {
        private CameraRaycaster _cameraRaycaster;
        private GameObject _walkTarget;
        private AICharacterControl _aiCharacterControl;

        private void Start ()
        {
            _aiCharacterControl = GetComponent<AICharacterControl>();
            _cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();

            _walkTarget = new GameObject("WalkTarget");
            _cameraRaycaster.onMouseOverTerrain += ProcessMouseOverTerrain;
            _cameraRaycaster.onMouseOverEnemy += MoveToEnemy;
        }

        private void ProcessMouseOverTerrain(Vector3 destination)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _walkTarget.transform.position = destination;
                _aiCharacterControl.SetTarget(_walkTarget.transform);
            }
        }

        private void MoveToEnemy(Enemy enemy)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(1))
            {
                _aiCharacterControl.SetTarget(enemy.transform);
            }
        }

        //TODO: Make it working
        //private void ProcessDirectMovement()
        //{
        //    float h = CrossPlatformInputManager.GetAxis("Horizontal");
        //    float v = CrossPlatformInputManager.GetAxis("Vertical");
        
        //    // calculate camera relative direction to move:
        //    Vector3 camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //    Vector3 move = v * camForward + h * Camera.main.transform.right;

        //    _thirdPersonCharacter.Move(move, false, false);
        //}
    }
}
