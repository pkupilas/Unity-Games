using UnityEngine;

namespace Weapons.Guns.Shotgun
{
    public class Shotgun : Weapon
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
            var shotgunData = weaponData as ShotgunData;
            if (shotgunData && !_ammunition.IsMagazineEmpty())
            {
                var newBullet = Instantiate(_ammunition.AmmunitionData.BulletData.BulletPrefab, transform.position + shotgunData.GripTransform.position, Quaternion.identity);
                var bulletRigidboy = newBullet.GetComponent<Rigidbody>();
                var bulletComponent = newBullet.GetComponent<Bullet>();

                bulletRigidboy.velocity = transform.forward * bulletComponent.BulletData.Velocity;
                _ammunition.RemoveBulletFromMagazine();
            }
        }
    }
}
