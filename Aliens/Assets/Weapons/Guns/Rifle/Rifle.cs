using System;
using UnityEngine;

namespace Weapons.Guns.Rifle
{
    public class Rifle : Weapon
    {
        private Ammunition _ammunition;

        protected override void Start()
        {
            base.Start();
            _ammunition = GetComponent<Ammunition>();
        }

        protected override void Shoot()
        {
            timer = 0f;
            var rifleData = weaponData as RifleData;
            if (rifleData && !_ammunition.IsMagazineEmpty())
            {
                var newBullet = Instantiate(_ammunition.AmmunitionData.BulletData.BulletPrefab, transform.position + rifleData.GripTransform.position, Quaternion.identity);
                var bulletRigidboy = newBullet.GetComponent<Rigidbody>();
                var bulletComponent = newBullet.GetComponent<Bullet>();

                bulletRigidboy.velocity = transform.forward * bulletComponent.BulletData.Velocity;
                _ammunition.RemoveBulletFromMagazine();
            }
        }
    }
}
