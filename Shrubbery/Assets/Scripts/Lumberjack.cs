using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Lumberjack : MonoBehaviour
{
    [SerializeField] private float _maxDistanceToTree = 1.6f;
    [SerializeField] private float _maxDistanceToSawmill = 1.6f;
    [SerializeField] private float _cutCooldown = 5f;

    private GameObject _sawmill;
    public GameObject Sawmill
    {
        get { return _sawmill; }
        set { _sawmill = value; }
    }

    private AICharacterControl _aiCharacterControl;
    private bool _hasTreeInBag;
    private float _timer;

    void Start()
    {
        _aiCharacterControl = GetComponent<AICharacterControl>();
    }

    public void ProcessTree(GameObject tree)
    {
        if (_hasTreeInBag)
        {

            if (Vector3.Distance(_sawmill.transform.position, transform.position) > _maxDistanceToSawmill)
            {
                _aiCharacterControl.SetTarget(_sawmill.transform);
            }
            else
            {
                GiveBackChoppedWood();
            }
        }
        else
        {
            if (Vector3.Distance(tree.transform.position, transform.position) > _maxDistanceToTree)
            {

                _aiCharacterControl.SetTarget(tree.transform);
                _timer = _cutCooldown;
            }
            else
            {
                ChopTree(tree);
            }
        }
    }

    public void ReturnAllWorkersToSawmill()
    {
        _aiCharacterControl.SetTarget(_sawmill.transform);
    }

    private void  GiveBackChoppedWood()
    {
        _hasTreeInBag = false;
    }

    private void ChopTree(GameObject tree)
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            _hasTreeInBag = false;
        }
        else
        {
            Destroy(tree);
            _hasTreeInBag = true;
        }
    }
}
