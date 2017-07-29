using UnityEngine;
using _Core;

namespace _Weapons.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _velocity;
        [SerializeField] private float _damage;
        [SerializeField] private GameObject _shooter;
    
        private void OnCollisionEnter(Collision other)
        {
            if (other == null || _shooter == null) return;

            var component = other.gameObject.GetComponent<IDamageable>();

            if (component != null && CheckTargetLayer(other))
            {
                component.TakeDamage(_damage);
            }

            Destroy(gameObject);
        }

        private bool CheckTargetLayer(Collision other)
        {
            return other.gameObject.layer != _shooter.layer;
        }

        public void SetVelocity(float value)
        {
            _velocity = value;
        }
        public void SetDamage(float value)
        {
            _damage = value;
        }
        public void SetShooter(GameObject shooter)
        {
            _shooter = shooter;
        }

        public float GetDefaultVelocity()
        {
            return _velocity;
        }
    }
}
