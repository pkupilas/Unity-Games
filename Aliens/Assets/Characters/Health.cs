using UnityEngine;
using WorldObjects.Spawner;

namespace Characters
{
    public class Health : MonoBehaviour
    {
        private CharacterData _characterData;
        private float _currentHealth;
        private EnemySpawner _enemySpawner;

        public float CurrentHealth => _currentHealth;

        void Start()
        {
            _characterData = GetComponent<Character>().CharacterData;
            _enemySpawner = FindObjectOfType<EnemySpawner>();
            SetCurrentHealth();
        }

        private void SetCurrentHealth()
        {
            _currentHealth = _characterData.MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, _characterData.MaxHealth);
            if (_currentHealth <= 0 && GetComponent<Enemy>())
            {
                Destroy(gameObject);
                _enemySpawner.IncNumberOfKilledInCurrentWave();
            }
        }

        public float GetHealthAsPercentage()
        {
            float maxHealth = _characterData.MaxHealth;
            return _currentHealth / maxHealth;
        }

        public bool CheckIfDead()
        {
            return _currentHealth <= 0;
        }

        public float GetMaxHealth()
        {
            return _characterData.MaxHealth;
        }
    }
}