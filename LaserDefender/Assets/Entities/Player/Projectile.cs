using UnityEngine;

namespace Entities.Player
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _damage = 100f;

        public float Damage => _damage;

        public void Hit()
        {
            Destroy(gameObject);
        }
    }
}
