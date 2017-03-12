using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{

    public int playerDamage;

    private Animator _animator;
    private Transform _target;
    private bool skipMove;


	// Use this for initialization
	protected override void Start ()
	{
        GameManager.instance.AddEnemyToList(this);
	    _animator = GetComponent<Animator>();
	    _target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
	}
	
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        if (skipMove)
        {
            skipMove = false;
            return;
        }
        base.AttemptMove<T>(xDir, yDir);
        skipMove = true;
    }

    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;

        if (Mathf.Abs(_target.transform.position.x - transform.position.x) < float.Epsilon)
        {
            yDir = (_target.position.y > transform.position.y) ? 1 : -1;
        }
        else
        {
            xDir = (_target.position.x > transform.position.x) ? 1 : -1;
        }
        AttemptMove<Player>(xDir, yDir);
    }

    protected override void OnCantMove<T>(T component)
    {
        Player hitPlayer = component as Player;

        _animator.SetTrigger("enemyAttack");

        hitPlayer.LoseFood(playerDamage);
    }
}
