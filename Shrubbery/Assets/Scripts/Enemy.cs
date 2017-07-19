using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private const float _MaxHealth = 100f;
    private float _currentHealth = _MaxHealth;

    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, _MaxHealth);
        if (CheckIfDie())
        {
            Destroy(gameObject);
        }
    }

    public bool CheckIfDie()
    {
        return _currentHealth <= 0;
    }
}
