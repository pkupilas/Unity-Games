using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Zombie : MonoBehaviour
{
    public float damage = 10;
    public float spawnRate = 1;
    public float speed = 2;
    public float pointsForKill = 100;
    public AudioClip deathSound;

    private Vector3 objectOfInterestPosition;
    private Base _base;
    private Player _player;
    private float _defaultSpeed;
    private Animator _animator;
    private GameObject _objectToAttack;
    private PointsManager _pointsManager;

    // Use this for initialization
    void Start ()
    {
        _defaultSpeed = speed;
        _player = FindObjectOfType<Player>();
	    _base = FindObjectOfType<Base>();
        _animator = GetComponent<Animator>();
        _pointsManager = FindObjectOfType<PointsManager>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    HandleMoving();
	    HandleRotation();
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Ammunition>() != null)
        {
            Destroy(coll.gameObject);
            Destroy(gameObject);
            _pointsManager.AddPoints(pointsForKill);
        }
        else if (coll.gameObject.GetComponent<Base>() != null)
        {
            _objectToAttack = coll.gameObject;
            StopMoving();
            StartAttacking();
        }
        else if (coll.gameObject.GetComponent<Player>() != null)
        {
            _objectToAttack = coll.gameObject;
            StopMoving();
            StartAttacking();
        }
    }

    public void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Player>() != null)
        {
            StopAttacking();
            StartMoving();
        }
    }

    private void StartMoving()
    {
        speed = _defaultSpeed;
        StopAttacking();
        _animator.SetBool("isWalking", true);
    }
    private void StopMoving()
    {
        speed = 0;
        _animator.SetBool("isWalking", false);
    }

    private void StartAttacking()
    {
        StopMoving();
        _animator.SetBool("isAttacking", true);
    }

    private void StopAttacking()
    {
        _animator.SetBool("isAttacking", false);
    }

    private void HandleMoving()
    {
        float step = speed * Time.deltaTime;

        UpdateObjectOfInterestPosition();
        transform.position = Vector3.MoveTowards(transform.position, objectOfInterestPosition, step);
    }

    private void UpdateObjectOfInterestPosition()
    {
        float distanceToPlayer = GetVectorLength(_player.transform.position);
        float distanceToBase = GetVectorLength(_base.transform.position);

        objectOfInterestPosition = distanceToPlayer > distanceToBase ? _base.transform.position : _player.transform.position;
    }

    private float GetVectorLength(Vector3 objTransform)
    {
        Vector3 length = objTransform - transform.position;

        return length.magnitude;
    }

    // Used in Animation as event
    private void DealDamage()
    {
        if (_objectToAttack.GetComponent<Base>())
        {
            DealDamageToBase(damage);
        }else if (_objectToAttack.GetComponent<Player>())
        {
            DealDamageToPlayer(damage);
        }
    }

    private void DealDamageToBase(float damage)
    {
        _base.ApplyDamage(damage);
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
    }

    private void DealDamageToPlayer(float damage)
    {
        _player.ApplyDamage(damage);
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
    }

    private void HandleRotation()
    {
        //TODO: Improve messy rotation code
        float RotationSpeed = 10f;
        if ((transform.position.x != objectOfInterestPosition.y) && (transform.position.y != objectOfInterestPosition.y))
        {
            transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.Euler(0, 0, Mathf.Atan2((objectOfInterestPosition.y - transform.position.y), (objectOfInterestPosition.x - transform.position.x)) * Mathf.Rad2Deg),
                    RotationSpeed * Time.deltaTime);
        }
}
}
