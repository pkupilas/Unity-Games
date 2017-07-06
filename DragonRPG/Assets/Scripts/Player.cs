using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _maxPlayerHealth = 100f;
    private float _currentPlayerHealth = 100f;

    public float HealthAsPercentage
    {
        get { return _currentPlayerHealth / _maxPlayerHealth; }
    }
}
