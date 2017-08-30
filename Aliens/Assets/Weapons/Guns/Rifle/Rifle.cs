using System;
using UnityEngine;
using Weapon.Ammunition;

namespace Weapons.Guns.Rifle
{
    public class Rifle : Weapon
    {
        protected override void Shoot()
        {
            timer = 0f;
            var rifleData = weaponData as RifleData;
            if (rifleData)
            {
                var newBullet = Instantiate(rifleData.AmmoTypePrefab, transform.position + rifleData.GripTransform.position, Quaternion.identity);
                var bulletRigidboy = newBullet.GetComponent<Rigidbody>();
                var bulletComponent = newBullet.GetComponent<Bullet>();

                bulletRigidboy.velocity = transform.forward * bulletComponent.BulletData.Velocity;
            }
        }
    }
}
