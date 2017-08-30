using System.Collections;
using Characters.Enemies;
using Characters.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using Weapons.Ammunition;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private float _attackRadius = 5f;

    private Player _player;
    private AICharacterControl _aiCharacterControl;
    private NavMeshAgent _navMeshAgent;
    private float _currentHealth;
    private bool _isAttacking;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _aiCharacterControl = GetComponent<AICharacterControl>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        SetEnemyValues();
    }

    void Update()
    {
        _aiCharacterControl.SetTarget(_player.transform);
        var distanceToPlayer = Vector3.Distance(_player.transform.position, gameObject.transform.position);
        Debug.Log(distanceToPlayer);
        if (distanceToPlayer <= _attackRadius && !_isAttacking)
        {
            _isAttacking = true;
            StartCoroutine(AttackTarget());
        }

        if (distanceToPlayer > _attackRadius)
        {
            _isAttacking = false;
            StopCoroutine(AttackTarget());
        }
        
    }

    private IEnumerator AttackTarget()
    {
        yield return new WaitForSecondsRealtime(_enemyData.AttackCooldown);
        _player.TakeDamage(_enemyData.Damage);
        _isAttacking = false;
    }


    private void SetEnemyValues()
    {
        _navMeshAgent.speed = _enemyData.Speed;
        _currentHealth = _enemyData.Health;
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

    private void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public float HealthAsPercentage()
    {
        float maxHealth = _enemyData.Health;
        return _currentHealth/maxHealth;
    }
}