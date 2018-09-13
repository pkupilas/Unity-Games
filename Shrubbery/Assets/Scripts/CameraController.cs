using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum CameraInput
    {
        Mouse,
        Keyboard,
        MouseAndKeyboard,
        Invalid
    }

    private const float DistanceFromScreenEdge = 10f;
    private const float MovingSpeed = 20f;
    private const float ZoomingSpeed = 1000f;
    private const float _MINIMAL_ORTHOGRAPHIC_SIZE = 5.0f;
    private const float _SCROLL_INVERTED = -1.0f;
    private Vector3 _initialCameraPosition = Vector3.zero;
    private Camera _camera;
    public CameraInput cameraInput = CameraInput.Keyboard;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _initialCameraPosition = gameObject.transform.position;
    }

    private void Update()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        var pan = GetCameraPositionBy();

        _camera.orthographicSize += _SCROLL_INVERTED * zoom * ZoomingSpeed * Time.deltaTime;

        _camera.orthographicSize = _camera.orthographicSize < _MINIMAL_ORTHOGRAPHIC_SIZE ?
            _MINIMAL_ORTHOGRAPHIC_SIZE : _camera.orthographicSize;

        // TODO: Make clamping
        gameObject.transform.Translate(pan);

        // Reset Camera
        if (Input.GetKey(KeyCode.Space))
        {
            gameObject.transform.position = _initialCameraPosition;
        }
    }

    private Vector3 GetCameraPositionBy()
    {
        var pan = Vector3.zero;

        switch (cameraInput)
        {
            case CameraInput.Invalid:
                return Vector3.zero;
            case CameraInput.Keyboard:
                pan = HandleByKeyboard();
                break;
            case CameraInput.Mouse:
                pan = HandleByMouse();
                break;
            //case CameraInput.MouseAndKeyboard:
            //    pan = HandleByMouseAndKeyboard();
            //    break;
            default:
                Debug.Log($"Camera input state not handled {cameraInput}");
                break;
        }


        return pan;
    }

    private static Vector3 HandleByKeyboard()
    {
        var pan = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
        {
            pan += new Vector3(MovingSpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            pan -= new Vector3(MovingSpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.W))
        {
            pan += new Vector3(0f, MovingSpeed * Time.deltaTime, 0f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            pan -= new Vector3(0f, MovingSpeed * Time.deltaTime, 0f);
        }

        return pan;
    }

    private static Vector3 HandleByMouse()
    {
        var pan = Vector3.zero;

        if (Input.mousePosition.x >= Screen.width - DistanceFromScreenEdge)
        {
            pan += new Vector3(MovingSpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.mousePosition.x <= DistanceFromScreenEdge)
        {
            pan -= new Vector3(MovingSpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.mousePosition.y >= Screen.height - DistanceFromScreenEdge)
        {
            pan += new Vector3(0f, MovingSpeed * Time.deltaTime, 0f);
        }

        if (Input.mousePosition.y <= DistanceFromScreenEdge)
        {
            pan -= new Vector3(0f, MovingSpeed * Time.deltaTime, 0f);
        }

        return pan;
    }

    //private void HandleByMouseAndKeyboard()
    //{
    //    return HandleByMouse(HandleByKeyboard());
    //}
}