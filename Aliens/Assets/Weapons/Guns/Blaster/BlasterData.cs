using UnityEngine;

namespace Weapons.Guns.Blaster
{
    [CreateAssetMenu(menuName = "Weapons/Blaster")]
    public class BlasterData : WeaponData
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _shotDuration;

        public float Damage => _damage;
        public float ShotDuration => _shotDuration;
    }
}