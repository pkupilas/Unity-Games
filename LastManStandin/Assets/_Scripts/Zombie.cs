﻿using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Zombie : MonoBehaviour
{
    public float damage = 10;
    public float spawnRate = 5;
    public float speed = 2;
    private Vector3 objectOfInterestPosition;
    private Base _base;
    private Player _player;
    private float _defaultSpeed;
    private Animator _animator;

    // Use this for initialization
    void Start ()
    {
        _defaultSpeed = speed;
        _player = FindObjectOfType<Player>();
	    _base = FindObjectOfType<Base>();
        _animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    HandleMoving();
	}

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Ammunition>() != null)
        {
            Destroy(coll.gameObject);
            Destroy(gameObject);
        }
        else if (coll.gameObject.GetComponent<Base>() != null)
        {
            // Stop moving
            StopMoving();
            // Attack base
            StartAttacking();
            // Deal damage to base
            DealDamageToBase(damage);
        }
        else if (coll.gameObject.GetComponent<Player>() != null)
        {
            // Stop moving
            StopMoving();
            // Attack player
            StartAttacking();
            // Deal damage to player
            DealDamageToPlayer(damage);
        }
    }

    public void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Player>() != null)
        {
            // Stop attack
            StopAttacking();
            // Start moving
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

        UpdateObjectOfInterest();
        transform.position = Vector3.MoveTowards(transform.position, objectOfInterestPosition, step);
    }

    private void UpdateObjectOfInterest()
    {
        float distanceToPlayer = GetVectorLength(_player.transform);
        float distanceToBase = GetVectorLength(_base.transform);

        objectOfInterestPosition = distanceToPlayer > distanceToBase ? _base.transform.position : _player.transform.position;
    }

    private float GetVectorLength(Transform objTransform)
    {
        Vector3 length = objTransform.position - transform.position;

        return length.magnitude;
    }

    private void DealDamageToBase(float damage)
    {
        _base.ApplyDamage(damage);
    }

    private void DealDamageToPlayer(float damage)
    {
        _player.ApplyDamage(damage);
    }
}