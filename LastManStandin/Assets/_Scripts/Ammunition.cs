using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour
{

    public float speed = 8f;

    private Player _player;
    private Quaternion _playerRotation;
    private Vector3 _directionVector;

	// Use this for initialization
	void Start ()
	{
	    _player = FindObjectOfType<Player>(); // possible nullptr
	    _playerRotation = _player.transform.rotation;
        RotateBullet();
        _directionVector = GetBulletDirectionVector();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    MoveBullet();
	}

    public void MoveBullet()
    {
        transform.position += speed*_directionVector;
    }

    private Vector3 GetBulletDirectionVector()
    {
        float v = 1;

        float rotationZ = transform.rotation.eulerAngles.z;

        if (rotationZ > 0 && rotationZ <= 90)
        {
            return new Vector3(v * Mathf.Cos(rotationZ * Mathf.Deg2Rad), v * Mathf.Sin(rotationZ * Mathf.Deg2Rad), 0);
        }
        else if (rotationZ > 90 && rotationZ <= 180)
        {
            float fixedRotation = rotationZ - 90;
            return new Vector3(-v * Mathf.Sin(fixedRotation * Mathf.Deg2Rad), v * Mathf.Cos(fixedRotation * Mathf.Deg2Rad), 0);
        }
        else if (rotationZ > 180 && rotationZ <= 270)
        {
            float fixedRotation = rotationZ - 180;
            return new Vector3(-v * Mathf.Cos(fixedRotation * Mathf.Deg2Rad), -v * Mathf.Sin(fixedRotation * Mathf.Deg2Rad), 0);
        }
        else if (rotationZ > 270 && rotationZ <= 360)
        {
            float fixedRotation = rotationZ - 270;
            return new Vector3(v * Mathf.Sin(fixedRotation * Mathf.Deg2Rad), -v * Mathf.Cos(fixedRotation * Mathf.Deg2Rad), 0);
        }

        return Vector3.zero;
    }

    public void RotateBullet()
    {
        transform.Rotate(_playerRotation.eulerAngles);
    }

}
