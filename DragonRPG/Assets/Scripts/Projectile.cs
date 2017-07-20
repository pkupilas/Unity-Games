using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float velocity;
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        var component = other.gameObject.GetComponent(typeof(IDamageable));
        if (component)
        {
            (component as IDamageable).TakeDamage(damage);
        }
    }
}
