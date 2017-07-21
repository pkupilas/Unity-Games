using Assets.Scripts.Utility;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _attackCooldown = 0.5f;
    [SerializeField] private float _maxAttackRange = 2f;

    private float _currentHealth;
    private float _lastHitTime;
    private GameObject _currentTarget;
    private CameraRaycaster _cameraRaycaster;


    void Start()
    {
        _cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        _cameraRaycaster.notifyMouseClickObservers += OnMouseClicked;
        _currentHealth = _maxHealth;
    }

    private void OnMouseClicked(RaycastHit raycastHit, int layerHit)
    {
        if (layerHit == Utilities.EnemyLayerNumber)
        {
            var enemy = raycastHit.collider.gameObject;

            if ((enemy.transform.position - transform.position).magnitude > _maxAttackRange)
            {
                return;
            }

            _currentTarget = enemy;

            if (Time.time - _lastHitTime > _attackCooldown)
            {
                _currentTarget.GetComponent<Enemy>().TakeDamage(_damage);
                _lastHitTime = Time.time;
            }
        }
    }

    public float HealthAsPercentage
    {
        get { return _currentHealth / _maxHealth; }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, _maxHealth);
    }
}
