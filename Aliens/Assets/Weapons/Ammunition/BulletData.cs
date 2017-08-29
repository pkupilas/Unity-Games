using UnityEngine;

namespace Weapons.Ammunition
{
    [CreateAssetMenu(menuName = "Armory/Bullets")]
    public class BulletData : ScriptableObject
    {
        [SerializeField] private float _velocity;
        [SerializeField] private float _damage;

        public float Velocity => _velocity;
        public float Damage => _damage;
    }
}