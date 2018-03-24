using System.Linq;
using ScriptableObjects;
using UnityEngine;

public class SimpleTurret : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private TurretConfig _turretConfig;

    private GameObject _target;
    private bool _isAttacking;
    private const string EnemyLayer = "Enemy";


    void Update ()
    {
        CheckForTargets();
        if (_target && !_isAttacking)
        {
            _isAttacking = true;
            InvokeRepeating(nameof(Shoot), 0, _turretConfig.FireRate);
        }

        if(_target==null)
        {
            _isAttacking = false;
            CancelInvoke(nameof(Shoot));
        }
	}

    private void Shoot()
    {
        GameObject newProjectile = Instantiate(_turretConfig.AmmunitionTypes, _spawnPoint.transform);
        var projectileComponent = newProjectile.GetComponent<Projectile>();
        var unitVectorToPlayer = (_target.transform.position - _spawnPoint.transform.position).normalized;
        
        newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileComponent.Velocity;
    }

    void OnDrawGizmos()
    {
        Color color = Color.yellow;
        color.a = 0.5f;
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, _turretConfig.Range);
    }

    void CheckForTargets()
    {
        var colliderList = Physics.OverlapSphere(transform.position, _turretConfig.Range, 1 << LayerMask.NameToLayer(EnemyLayer)).ToList();

        if (_target==null || !colliderList.Contains(_target.GetComponent<Collider>()))
        {
            _target = colliderList.Count > 0 ? colliderList[0].gameObject : null;
        }
    }
}
