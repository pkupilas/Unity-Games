using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _walkMoveStopRadius = 0.2f;

    private bool _isInDirectMode = false;
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
        //TODO: allow player to map later or add to menu
        if (Input.GetKeyDown(KeyCode.G))
        {
            _isInDirectMode = !_isInDirectMode;
        }

        if (_isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            ProcessMouseMovement();
        }
    }

    private void ProcessDirectMovement()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        
        
        // calculate camera relative direction to move:
        Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;

        _character.Move(m_Move, false, false);
    }

    private void ProcessMouseMovement()
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
