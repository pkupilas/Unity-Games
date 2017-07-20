using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _velocity = 4f;
    [SerializeField] private float _damage = 33f;

    private Rigidbody _rigidbody;
    
	void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody>();
	    _rigidbody.velocity = new Vector3(-1 * _velocity, 0, 0);
	}
	
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
