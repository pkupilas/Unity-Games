using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private float _maxEnemyHealth = 100f;
    private float _currentEnemyHealth = 100f;

    public float HealthAsPercentage
    {
        get { return _currentEnemyHealth / _maxEnemyHealth; }
    }
}
