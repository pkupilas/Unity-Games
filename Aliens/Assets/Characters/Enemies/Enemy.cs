using System.Collections;
using Characters;
using Characters.Enemies;
using Characters.Player;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] private AnimatorOverrideController _animatorOverrideController;

    private AICharacterControl _aiCharacterControl;
    private NavMeshAgent _navMeshAgent;
    private const string AttackTrigger = "AttackTrigger";
    private const string AttackAnimationName = "DefaultAttack";
    private Animator _animator;

    protected Player player;
    protected bool isAttacking;

    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();
        _aiCharacterControl = GetComponent<AICharacterControl>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        SetNavMeshAgentSpeed();
        SetAnimator();
    }

    private void SetAnimator()
    {
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = _animatorOverrideController;
        _animatorOverrideController[AttackAnimationName] = enemyData.AttackAnimationClip;
    }

    protected virtual void Update()
    {
        var distanceToPlayer = Vector3.Distance(player.transform.position, gameObject.transform.position);

        if (distanceToPlayer <= enemyData.AttackRadius && !isAttacking)
        {
            _animator.SetTrigger(AttackTrigger);
            StartCoroutine(AttackTarget());
        }

        if (distanceToPlayer > enemyData.AttackRadius)
        {
            isAttacking = false;
            StopCoroutine(AttackTarget());
        }

        _aiCharacterControl.SetTarget(distanceToPlayer > enemyData.AttackRadius ? player.transform : transform);
    }

    protected abstract IEnumerator AttackTarget();


    private void SetNavMeshAgentSpeed()
    {
        _navMeshAgent.speed = enemyData.Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        var bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet)
        {
            var healthComponenet = GetComponent<Health>();
            healthComponenet.TakeDamage(bullet.BulletData.Damage);
            Destroy(bullet.gameObject);
        }
    }
}