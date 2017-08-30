using UnityEngine;
using Weapon.Ammunition;

namespace Weapons.Guns.Shotgun
{
    public class Shotgun : Weapon
    {
        protected override void Shoot()
        {
            timer = 0f;
            var shotgunData = weaponData as ShotgunData;
            if (shotgunData)
            {
                var newBullet = Instantiate(shotgunData.AmmoTypePrefab, transform.position + shotgunData.GripTransform.position, Quaternion.identity);
                var bulletRigidboy = newBullet.GetComponent<Rigidbody>();
                var bulletComponent = newBullet.GetComponent<Bullet>();

                bulletRigidboy.velocity = transform.forward * bulletComponent.BulletData.Velocity;
            }
        }
    }
}
