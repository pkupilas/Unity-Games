using UnityEngine;

namespace Characters
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private CharacterData _characterData;
        private float _currentHealth;

        public float CurrentHealth => _currentHealth;

        void Start()
        {
            SetCurrentHealth();
        }

        private void SetCurrentHealth()
        {
            _currentHealth = _characterData.MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, _characterData.MaxHealth);
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        public float GetHealthAsPercentage()
        {
            float maxHealth = _characterData.MaxHealth;
            return _currentHealth / maxHealth;
        }
    }
}