using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;
using Assets.Scripts.Utility;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof(ThirdPersonCharacter))]
[RequireComponent(typeof(CameraRaycaster))]
public class PlayerMovement : MonoBehaviour
{
    
    private ThirdPersonCharacter _thirdPersonCharacter;
    private CameraRaycaster _cameraRaycaster;
    private GameObject _walkTarget;
    private AICharacterControl _aiCharacterControl;

    private void Start ()
	{
        _thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
	    _aiCharacterControl = GetComponent<AICharacterControl>();
        _cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();

        _walkTarget = new GameObject("WalkTarget");
	    _cameraRaycaster.notifyMouseClickObservers += ProcessMouseMovement;
	}

    void ProcessMouseMovement(RaycastHit raycasthit, int layerhit)
    {
        switch (layerhit)
        {
            case Utilities.WalkableLayerNumber:
                _walkTarget.transform.position = raycasthit.point;
                _aiCharacterControl.SetTarget(_walkTarget.transform);
                break;
            case Utilities.EnemyLayerNumber:
                GameObject enemy = raycasthit.collider.gameObject;
                _aiCharacterControl.SetTarget(enemy.transform);
                break;
            default:
                Debug.LogWarning("ProcessMouseMovement - unknown layer number.");
                return;
        }
    }

    //TODO: Make it working
    private void ProcessDirectMovement()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        
        // calculate camera relative direction to move:
        Vector3 camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = v * camForward + h * Camera.main.transform.right;

        _thirdPersonCharacter.Move(move, false, false);
    }
}
