using Characters.Player;
using UnityEngine;
using Weapons.Ammunition;

namespace Weapons.Rifle
{
    public class Rifle : MonoBehaviour
    {
        [SerializeField] private GameObject _ammoType;
        [SerializeField] private Transform _weaponGrip;

        private Player _player;

        public Transform WeaponGrip => _weaponGrip;

        void Start()
        {
            _player = FindObjectOfType<Player>();
        }

        public void Shoot()
        {
            var newBullet = Instantiate(_ammoType, _player.transform.position + Vector3.up, Quaternion.identity);
            var bulletRigidboy = newBullet.GetComponent<Rigidbody>();
            var bulletComponent = newBullet.GetComponent<Bullet>();
            bulletRigidboy.velocity = transform.forward * bulletComponent.Velocity;
        }
    }
}
