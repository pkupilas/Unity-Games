using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sawmill : MonoBehaviour
{

    [SerializeField] private float _range = 10f;
    [SerializeField] private GameObject _workerPrefab;
    [SerializeField] private GameObject _workerSpawnPoint;

    private GameObject _target;
    private GameObject _worker;

    void Start()
    {
        CreateWorker();
    }

    void Update ()
	{
	    CheckForTargets();
	    ManageWorkers();
	}

    private void CreateWorker()
    {
        _worker = Instantiate(_workerPrefab, _workerSpawnPoint.transform.position, Quaternion.identity);
        _worker.GetComponent<Lumberjack>().Sawmill = gameObject;
    }
    
    private void CheckForTargets()
    {
        var colliderList = Physics.OverlapSphere(transform.position, _range, 1 << LayerMask.NameToLayer("Tree")).ToList();

        if (colliderList.Count>0 && (_target == null || !colliderList.Contains(_target.GetComponent<Collider>())))
        {
            var closestTarget =
                colliderList.OrderBy(x => Vector3.Distance(x.gameObject.transform.position, _worker.transform.position)).ToList().First();
            _target = colliderList.Count > 0 ? closestTarget.gameObject : null;
        }
    }

    private void ManageWorkers()
    {
        if (_target)
        {
            _worker.GetComponent<Lumberjack>().ProcessTree(_target);
        }
    }

    private void OnDrawGizmos()
    {
        Color color = Color.black;
        color.a = 0.5f;
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
