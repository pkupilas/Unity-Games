using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sawmill : MonoBehaviour
{

    [SerializeField] private float _range = 10f;
    private GameObject _target;


	void Update ()
	{
	    CheckForTargets();
	}
    
    void OnDrawGizmos()
    {
        Color color = Color.black;
        color.a = 0.5f;
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    void CheckForTargets()
    {
        var colliderList = Physics.OverlapSphere(transform.position, _range, 1 << LayerMask.NameToLayer("Tree")).ToList();

        if (_target == null || !colliderList.Contains(_target.GetComponent<Collider>()))
        {
            _target = colliderList.Count > 0 ? colliderList[0].gameObject : null;
            print("iamgere");
        }
    }

}
