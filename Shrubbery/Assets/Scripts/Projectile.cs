using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _velocity = 4f;
    [SerializeField] private float _damage = 1f;

    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
    public float Velocity
    {
        get { return _velocity; }
        set { _velocity = value; }
    }

    private Rigidbody _rigidbody;
    
    void OnTriggerEnter(Collider other)
    {
        var component = other.gameObject.GetComponent(typeof(IDamageable));
        if (component)
        {
            (component as IDamageable).TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
