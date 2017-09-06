using System.Collections;
using Characters;
using Characters.Enemies;
using Characters.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;

    private Player _player;
    private AICharacterControl _aiCharacterControl;
    private NavMeshAgent _navMeshAgent;
    private bool _isAttacking;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _aiCharacterControl = GetComponent<AICharacterControl>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        SetNavMeshAgentSpeed();
    }

    void Update()
    {
        _aiCharacterControl.SetTarget(_player.transform);
        var distanceToPlayer = Vector3.Distance(_player.transform.position, gameObject.transform.position);
        if (distanceToPlayer <= _enemyData.AttackRadius && !_isAttacking)
        {
            _isAttacking = true;
            StartCoroutine(AttackTarget());
        }

        if (distanceToPlayer > _enemyData.AttackRadius)
        {
            _isAttacking = false;
            StopCoroutine(AttackTarget());
        }
    }

    private IEnumerator AttackTarget()
    {
        yield return new WaitForSecondsRealtime(_enemyData.AttackCooldown);
        _player.GetComponent<Health>().TakeDamage(_enemyData.Damage);
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