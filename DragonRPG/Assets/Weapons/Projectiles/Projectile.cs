using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float velocity;
    public float damage;

    private void OnCollisionEnter(Collision other)
    {
        var component = other.gameObject.GetComponent(typeof(IDamageable));
        if (component)
        {
            (component as IDamageable).TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
