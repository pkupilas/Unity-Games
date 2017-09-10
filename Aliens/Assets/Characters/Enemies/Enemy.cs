using System.Collections;
using Characters;
using Characters.Enemies;
using Characters.Player;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : Character
{
    [SerializeField] private AnimatorOverrideController _animatorOverrideController;

    private AICharacterControl _aiCharacterControl;
    private NavMeshAgent _navMeshAgent;
    private const string AttackTrigger = "AttackTrigger";
    private const string AttackAnimationName = "DefaultAttack";
    private Animator _animator;
    private Rigidbody _rigidbody;
    
    protected Player player;
    protected bool isAttacking;
    protected AudioSource audioSource;

    protected virtual void Start()
    {
        _aiCharacterControl = GetComponent<AICharacterControl>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        SetNavMeshAgentSpeed();
        SetAnimator();
    }

    private void SetAnimator()
    {
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = _animatorOverrideController;
        _animatorOverrideController[AttackAnimationName] = (characterData as EnemyData).AttackAnimationClip;
    }

    protected virtual void Update()
    {
        if (player)
        {
            var distanceToPlayer = Vector3.Distance(player.transform.position, gameObject.transform.position);

            if (distanceToPlayer <= (characterData as EnemyData).AttackRadius && !isAttacking)
            {
                _animator.SetTrigger(AttackTrigger);
                StartCoroutine(AttackTarget());
            }

            if (distanceToPlayer > (characterData as EnemyData).AttackRadius)
            {
                isAttacking = false;
                StopCoroutine(AttackTarget());
            }

            _aiCharacterControl.SetTarget(distanceToPlayer > (characterData as EnemyData).AttackRadius
                ? player.transform
                : transform);
            _rigidbody.MoveRotation(Quaternion.LookRotation(player.transform.position - transform.position));
        }
        else
        {
            player = FindObjectOfType<Player>();
        }
    }

    protected abstract IEnumerator AttackTarget();


    private void SetNavMeshAgentSpeed()
    {
        _navMeshAgent.speed = (characterData as EnemyData).Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        var bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet)
        {
            TakeDamage(bullet.BulletData.Damage);
            Destroy(bullet.gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        var healthComponent = GetComponent<Health>();
        healthComponent.TakeDamage(damage);
    }
}