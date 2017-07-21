using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Lumberjack : MonoBehaviour
{
    [SerializeField] private float _maxDistanceToTree = 1.6f;

    private GameObject _sawmill;
    public GameObject Sawmill
    {
        get { return _sawmill; }
        set { _sawmill = value; }
    }

    private AICharacterControl _aiCharacterControl;
    private NavMeshAgent _navMeshAgent;

    void Start()
    {
        _aiCharacterControl = GetComponent<AICharacterControl>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void ProcessTree(GameObject tree)
    {
        print(Vector3.Distance(tree.transform.position, transform.position));

        if (Vector3.Distance(tree.transform.position, transform.position) > _maxDistanceToTree)
        {
            _aiCharacterControl.SetTarget(tree.transform);
        }
        else
        {
            Destroy(tree);
            _aiCharacterControl.SetTarget(_sawmill.transform);
        }
    }
}
