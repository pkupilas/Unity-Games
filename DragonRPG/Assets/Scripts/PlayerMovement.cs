using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _walkMoveStopRadius = 0.2f;
    private ThirdPersonCharacter _character;
    private CameraRaycaster _cameraRaycaster;
    private Vector3 _currentClickTarget;


	// Use this for initialization
	private void Start ()
	{
	    _character = GetComponent<ThirdPersonCharacter>();
	    _cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
	    _currentClickTarget = transform.position;
	}
	
	// Update is called once per frame
	private void FixedUpdate ()
	{
	    if (Input.GetMouseButton(0))
	    {
            switch (_cameraRaycaster.layerHit)
            {
                case Layer.Enemy:
                    Debug.Log("Not moving enemy.");
                    break;
                case Layer.Walkable:
                    _currentClickTarget = _cameraRaycaster.hit.point;
                    break;
                default:
                    Debug.Log("Unexpected layer found.");
                    return;
            }
        }

	    var playerToClickPoint = _currentClickTarget - transform.position;

	    if (playerToClickPoint.magnitude >= _walkMoveStopRadius)
	    {
	        _character.Move(playerToClickPoint, false, false);
	    }
	    else
	    {
            _character.Move(Vector3.zero, false, false);
        }
    }
}
