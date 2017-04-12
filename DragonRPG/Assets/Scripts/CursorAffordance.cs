using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour
{

    private CameraRaycaster _cameraRaycaster;

    [SerializeField] private Texture2D _walkTexture = null;
    [SerializeField] private Texture2D _attackTexture = null;
    [SerializeField] private Texture2D _questionTexture = null;
    [SerializeField] private Vector2 _hotspot = new Vector2(96, 96);

    // Use this for initialization
    void Start ()
	{
	    _cameraRaycaster = GetComponent<CameraRaycaster>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    switch (_cameraRaycaster.layerHit)
	    {
	        case Layer.Walkable:
                Cursor.SetCursor(_walkTexture, _hotspot, CursorMode.Auto);
	            break;
            case Layer.Enemy:
                Cursor.SetCursor(_attackTexture, _hotspot, CursorMode.Auto);
	            break;
            case Layer.RaycastEndStop:
                Cursor.SetCursor(_questionTexture, _hotspot, CursorMode.Auto);
	            break;
            default:
                Debug.LogError("Unknown layer");
	            break;

	    }
	}
}
