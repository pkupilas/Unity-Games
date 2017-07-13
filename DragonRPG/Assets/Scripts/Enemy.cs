using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour {

    [SerializeField] private float _maxEnemyHealth = 100f;
    [SerializeField] private float _followRadius = 10f;

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
        if (Vector3.Distance(_player.transform.position, gameObject.transform.position) <= _followRadius)
        {
            _aiCharacterControl.SetTarget(_player.gameObject.transform);
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
}
