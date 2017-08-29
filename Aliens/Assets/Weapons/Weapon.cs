using Characters.Player;
using UnityEngine;
using Weapons.Ammunition;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponData _weaponData;

        private Player _player;
        
        public WeaponData WeaponData => _weaponData;

        void Start()
        {
            _player = FindObjectOfType<Player>();
        }

        public void Shoot()
        {
            var offset = _player.transform.position + Vector3.up;
            var newBullet = Instantiate(_weaponData.AmmoTypePrefab, offset, Quaternion.identity);
            var bulletRigidboy = newBullet.GetComponent<Rigidbody>();
            var bulletComponent = newBullet.GetComponent<Bullet>();

            bulletRigidboy.velocity = transform.forward * bulletComponent.BulletData.Velocity;
        }
    }
}
