using System.Linq;
using UnityEngine;

public class SimpleTurret : MonoBehaviour
{
    [SerializeField] private GameObject _ammunitionType;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float _range = 2f;

    private GameObject _target;
    private bool isAttacking;
    private const string EnemyLayer = "Enemy";


    void Update ()
    {
        CheckForTargets();
        if (_target && !isAttacking)
        {
            isAttacking = true;
            InvokeRepeating(nameof(Shoot), 0, _fireRate);
        }

        if(_target==null)
        {
            isAttacking = false;
            CancelInvoke(nameof(Shoot));
        }
	}

    private void Shoot()
    {
        GameObject newProjectile = Instantiate(_ammunitionType, _spawnPoint.transform);
        var projectileComponent = newProjectile.GetComponent<Projectile>();
        var unitVectorToPlayer = (_target.transform.position - _spawnPoint.transform.position).normalized;
        
        newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileComponent.Velocity;
    }

    void OnDrawGizmos()
    {
        Color color = Color.yellow;
        color.a = 0.5f;
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position,_range);
    }

    void CheckForTargets()
    {
        var colliderList = Physics.OverlapSphere(transform.position, _range, 1 << LayerMask.NameToLayer(EnemyLayer)).ToList();

        if (_target==null || !colliderList.Contains(_target.GetComponent<Collider>()))
        {
            _target = colliderList.Count > 0 ? colliderList[0].gameObject : null;
        }
    }
}
