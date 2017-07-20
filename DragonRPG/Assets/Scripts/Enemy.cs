using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable
{

    [SerializeField] private float _maxEnemyHealth = 100f;
    [SerializeField] private float _followRadius = 10f;
    [SerializeField] private float _attackRadius = 5f;
    [SerializeField] private float _attackDamage = 10f;

    [SerializeField] private GameObject _projectileToUse;
    [SerializeField] private GameObject _projectileSpawnPoint;

    private float _currentEnemyHealth = 100f;
    private Player _player;
    private AICharacterControl _aiCharacterControl;

    void Awake()
    {
        _aiCharacterControl = GetComponent<AICharacterControl>();
        _player = FindObjectOfType<Player>();
    }

    void Update()
    {

        if (Vector3.Distance(_player.transform.position, gameObject.transform.position) <= _attackRadius)
        {
            SpawnProjectile();
        }

        if (Vector3.Distance(_player.transform.position, gameObject.transform.position) <= _followRadius)
        {
            _aiCharacterControl.SetTarget(_player.transform);
        }
        else
        {
            _aiCharacterControl.SetTarget(transform);
        }

    }
    
    public float HealthAsPercentage
    {
        get { return _currentEnemyHealth / _maxEnemyHealth; }
    }

    public void TakeDamage(float damage)
    {
        _currentEnemyHealth = Mathf.Clamp(_currentEnemyHealth - damage, 0f, _maxEnemyHealth);
    }

    private void OnDrawGizmos()
    {
        // Attack sphere  
        Gizmos.color = new Color(255f, 0f, 0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, _attackRadius);

        // Follow sphere  
        Gizmos.color = new Color(0f, 0f, 255f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, _followRadius);
    }

    private void SpawnProjectile()
    {
        var newProjectile = Instantiate(_projectileToUse, _projectileSpawnPoint.transform.position, Quaternion.identity);
        var projectileComponent = newProjectile.GetComponent<Projectile>();
        var playerFixedPosition = _player.transform.position + new Vector3(0, 2f, 0);
        var unitVectorToPlayer = (playerFixedPosition - _projectileSpawnPoint.transform.position).normalized;

        projectileComponent.damage = _attackDamage;
        newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileComponent.velocity;
    }
}