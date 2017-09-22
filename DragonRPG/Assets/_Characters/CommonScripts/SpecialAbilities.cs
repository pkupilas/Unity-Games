using System.Collections.Generic;
using UnityEngine;
using _Characters.Abilities;

namespace _Characters.CommonScripts
{
    public class SpecialAbilities : MonoBehaviour
    {
        [SerializeField] private float _maxEnergy = 100f;
        [SerializeField] private float _energyRegenerationPointsPerSecond = 10f;
        [SerializeField] private List<AbilityConfig> _specialAbilities;

        private float _currentEnergy;

        public float EnergyAsPercentage => _currentEnergy / _maxEnergy;
        public int SpecialAbilitiesCount => _specialAbilities.Count;

        void Start ()
        {
            SetCurrentEnergyToMax();
            AttachAvailableAbilities();
        }

        void Update()
        {
            RegenerateEnergy();
        }

        private void SetCurrentEnergyToMax()
        {
            _currentEnergy = _maxEnergy;
        }

        private void RegenerateEnergy()
        {
            if (_currentEnergy < _maxEnergy)
            {
                _currentEnergy += _energyRegenerationPointsPerSecond * Time.deltaTime;
                _currentEnergy = Mathf.Clamp(_currentEnergy, 0f, _maxEnergy);
            }
        }

        private void AttachAvailableAbilities()
        {
            foreach (var specialAbility in _specialAbilities)
            {
                specialAbility.AttachAbilityTo(gameObject);
            }
        }

        public void AttemptSpecialAbility(int abilityIndex, AbilityParams abilityParams)
        {
            float energyCost = _specialAbilities[abilityIndex].EnergyCost;

            if (energyCost <= _currentEnergy)
            {
                ProcessEnergy(energyCost);
                var specialAbilityParams = abilityParams;
                _specialAbilities[abilityIndex].UseAbility(specialAbilityParams);
            }
        }

        public void ProcessEnergy(float amount)
        {
            float newCurrentEnergy = _currentEnergy - amount;
            _currentEnergy = Mathf.Clamp(newCurrentEnergy, 0f, _maxEnergy);
        }
    }
}