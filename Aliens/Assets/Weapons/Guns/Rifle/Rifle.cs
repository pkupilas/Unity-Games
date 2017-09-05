using System;
using Assets.Weapons.Guns;
using UnityEngine;

namespace Weapons.Guns.Rifle
{
    public class Rifle : Firearm
    {
        protected override void Shoot()
        {
            timer = 0f;
            var rifleData = weaponData as RifleData;
            if (rifleData && !ammunition.IsMagazineEmpty())
            {
                var newBullet = Instantiate(ammunition.AmmunitionData.BulletData.BulletPrefab, transform.position + rifleData.GripTransform.position, Quaternion.identity);
                var bulletRigidboy = newBullet.GetComponent<Rigidbody>();
                var bulletComponent = newBullet.GetComponent<Bullet>();

                bulletRigidboy.velocity = transform.forward * bulletComponent.BulletData.Velocity;
                ammunition.RemoveBulletFromMagazine();
            }
        }
    }
}
