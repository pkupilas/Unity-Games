using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof(ThirdPersonCharacter))]
[RequireComponent(typeof(CameraRaycaster))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _walkMoveStopRadius = 0.2f;
    [SerializeField] private float _attackMoveStopRadius = 5f;

    private bool _isInDirectMode = false;
    private ThirdPersonCharacter _character;
    private CameraRaycaster _cameraRaycaster;
    private Vector3 _currentDestination;
    private Vector3 _clickPoint;


    private void Start ()
	{
	    _character = GetComponent<ThirdPersonCharacter>();
	    _cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
	    _currentDestination = transform.position;
	}
	
    private void ProcessDirectMovement()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        
        // calculate camera relative direction to move:
        Vector3 camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = v * camForward + h * Camera.main.transform.right;

        _character.Move(move, false, false);
    }

    //private void ProcessMouseMovement()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        _clickPoint = _cameraRaycaster.Hit.point;
    //        switch (_cameraRaycaster.LayerHit)
    //        {
    //            case Layer.Enemy:
    //                _currentDestination = ShortDestination(_clickPoint, _attackMoveStopRadius);
    //                break;
    //            case Layer.Walkable:
    //                _currentDestination = ShortDestination(_clickPoint, _walkMoveStopRadius);
    //                break;
    //            default:
    //                Debug.Log("Unexpected layer found.");
    //                return;
    //        }
    //    }

    //    WalkToDestination();
    //}

    private void WalkToDestination()
    {
        var playerToClickPoint = _currentDestination - transform.position;

        _character.Move(playerToClickPoint.magnitude >= 0 ? playerToClickPoint : Vector3.zero, false, false);
    }

    private Vector3 ShortDestination(Vector3 destination, float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    private void OnDrawGizmos()
    {
        // Walk line 
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, _clickPoint);
        Gizmos.DrawSphere(_currentDestination, 0.1f);
        Gizmos.DrawSphere(_clickPoint, 0.15f);

        // Attack sphere 
        Gizmos.color = new Color(255f, 0f, 0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position,_attackMoveStopRadius);
    }
}
