using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    [SerializeField] private RawImage _energyBar;
    [SerializeField] private float _maxEnergy;
    [SerializeField] private float _energyRegenerationPointsPerSecond = 10f;

    private float _currentEnergy;

	void Start ()
	{
	    _currentEnergy = _maxEnergy;
    }

    void Update()
    {
        RegenerateEnergy();
    }

    private void RegenerateEnergy()
    {
        if (_currentEnergy < _maxEnergy)
        {
            _currentEnergy += _energyRegenerationPointsPerSecond * Time.deltaTime;
            _currentEnergy = Mathf.Clamp(_currentEnergy, 0f, _maxEnergy);
            UpdateEnergyBar();
        }
    }


    public bool IsEnergyAvailable(float amount)
    {
        return amount <=_currentEnergy;
    }

    public void ProcessEnergy(float amount)
    {
        float newCurrentEnergy = _currentEnergy - amount;
        _currentEnergy = Mathf.Clamp(newCurrentEnergy, 0f, _maxEnergy);
        UpdateEnergyBar();
    }

    //TODO: get rid of magic numbers
    private void UpdateEnergyBar()
    {
        float xValue = -(EnergyAsPercentage() / 2f) - 0.5f;
        _energyBar.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }

    private float EnergyAsPercentage()
    {
        return _currentEnergy / _maxEnergy;
    }
}
