using System.Linq;
using UnityEngine;

public class SimpleTurret : MonoBehaviour
{
    [SerializeField] private GameObject _ammunition;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private float _range = 2f;
    private GameObject _target;


    void Update ()
    {
        CheckForTargets();
        if (_target)
        {
            Shoot();
        }
	}

    private void Shoot()
    {
        GameObject projectile = Instantiate(_ammunition,_spawnPoint.transform);
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
        var colliderList = Physics.OverlapSphere(transform.position, _range, 1 << LayerMask.NameToLayer("Enemy")).ToList();

        if (_target==null || !colliderList.Contains(_target.GetComponent<Collider>()))
        {
            _target = colliderList.Count > 0 ? colliderList[0].gameObject : null;
        }
    }
}
