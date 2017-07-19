using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    [SerializeField] private float _maxPlayerHealth = 100f;
    private float _currentPlayerHealth = 100f;

    public float HealthAsPercentage
    {
        get { return _currentPlayerHealth / _maxPlayerHealth; }
    }

    public void TakeDamage(float damage)
    {
        _currentPlayerHealth = Mathf.Clamp(_currentPlayerHealth - damage, 0f, _maxPlayerHealth);
    }
}
