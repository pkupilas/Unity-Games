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
        float x = 1;
        float y = 1;

        float rotationZ = transform.rotation.eulerAngles.z;

        if (rotationZ > 340f || rotationZ <= 20f)
        {
            return new Vector3(-x, 0, 0);
        }
        else if (rotationZ > 20f && rotationZ <= 70f)
        {
            return new Vector3(-x, -y, 0);
        }
        else if (rotationZ > 70f && rotationZ <= 110f)
        {
            return new Vector3(0, -y, 0);
        }
        else if (rotationZ > 110f && rotationZ <= 160f)
        {
            return new Vector3(x, -y, 0);
        }
        else if (rotationZ > 160f && rotationZ <= 200f)
        {
            return new Vector3(x, 0, 0);
        }
        else if (rotationZ > 200f && rotationZ <= 250f)
        {
            return new Vector3(x, y, 0);
        }
        else if (rotationZ > 250f && rotationZ <= 290f)
        {
            return new Vector3(0, y, 0);
        }
        else if (rotationZ > 290f && rotationZ <= 340f)
        {
            return new Vector3(-x, y, 0);
        }

        return Vector3.zero;
    }

    public void RotateBullet()
    {
        transform.Rotate(_playerRotation.eulerAngles + new Vector3(0, 0, 180f));
    }

}
