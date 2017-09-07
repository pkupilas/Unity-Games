using System.Collections;
using Characters;
using Characters.Enemies;
using Characters.Player;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private AnimatorOverrideController _animatorOverrideController;

    private Player _player;
    private AICharacterControl _aiCharacterControl;
    private NavMeshAgent _navMeshAgent;
    private bool _isAttacking;
    private const string AttackTrigger = "AttackTrigger";
    private const string AttackAnimationName = "DefaultAttack";
    private Animator _animator;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _aiCharacterControl = GetComponent<AICharacterControl>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        SetNavMeshAgentSpeed();
        SetAnimator();
    }

    private void SetAnimator()
    {
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = _animatorOverrideController;
        _animatorOverrideController[AttackAnimationName] = _enemyData.AttackAnimationClip;
    }

    void Update()
    {
        //_aiCharacterControl.SetTarget(_player.transform);
        var distanceToPlayer = Vector3.Distance(_player.transform.position, gameObject.transform.position);
        if (distanceToPlayer <= _enemyData.AttackRadius && !_isAttacking)
        {
            _animator.SetTrigger(AttackTrigger);
            StartCoroutine(AttackTarget());
        }

        if (distanceToPlayer > _enemyData.AttackRadius)
        {
            _isAttacking = false;
            StopCoroutine(AttackTarget());
        }

        _aiCharacterControl.SetTarget(distanceToPlayer > _enemyData.AttackRadius ? _player.transform : transform);
    }

    private IEnumerator AttackTarget()
    {
        _isAttacking = true;
        _player.GetComponent<Health>().TakeDamage(_enemyData.Damage);
        yield return new WaitForSeconds(_enemyData.AttackCooldown);
        _isAttacking = false;
    }


    private void SetNavMeshAgentSpeed()
    {
        _navMeshAgent.speed = _enemyData.Speed;
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