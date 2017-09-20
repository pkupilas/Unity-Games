using UnityEngine;
using UnityEngine.UI;

namespace _Characters
{
    public class Energy : MonoBehaviour
    {
        [SerializeField] private Image _energyOrb;
        [SerializeField] private float _maxEnergy;
        [SerializeField] private float _energyRegenerationPointsPerSecond = 10f;

        private float _currentEnergy;
        private float EnergyAsPercentage => _currentEnergy / _maxEnergy;

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
                UpdateEnergyOrb();
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
            UpdateEnergyOrb();
        }
        
        private void UpdateEnergyOrb()
        {
            _energyOrb.fillAmount = EnergyAsPercentage;
        }
    }
}