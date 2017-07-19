using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float _damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        var component = other.gameObject.GetComponent(typeof(IDamageable));
        if (component)
        {
            (component as IDamageable).TakeDamage(_damage);
        }
    }
}
