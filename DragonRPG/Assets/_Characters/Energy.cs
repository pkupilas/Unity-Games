using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    [SerializeField] private RawImage _energyBar;
    [SerializeField] private float _maxEnergy;

    private float _currentEnergy;

	void Start ()
	{
	    _currentEnergy = _maxEnergy;
	}
}
