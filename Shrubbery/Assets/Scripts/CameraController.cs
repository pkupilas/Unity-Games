using UnityEngine;


public class CameraController : MonoBehaviour
{
    private const float DistanceFromScreenEdge = 10f;
    private const float MovingSpeed = 20f;
    private const float ZoomingSpeed = 1000f;
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update ()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        var pan = Vector3.zero;

        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - DistanceFromScreenEdge)
        {
            pan += new Vector3(MovingSpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= DistanceFromScreenEdge)
        {
            pan -= new Vector3(MovingSpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - DistanceFromScreenEdge)
        {
            pan += new Vector3(0f, MovingSpeed * Time.deltaTime, 0f);
        }

        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= DistanceFromScreenEdge)
        {
            pan -= new Vector3(0f, MovingSpeed * Time.deltaTime, 0f);
        }

        _camera.orthographicSize += zoom * ZoomingSpeed * Time.deltaTime;

        // TODO: Make clamping
        gameObject.transform.Translate(pan);
    }
}
