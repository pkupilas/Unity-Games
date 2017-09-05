using Assets.Weapons.Guns;
using UnityEngine;

namespace Weapons.Guns.Shotgun
{
    public class Shotgun : Firearm
    {
        protected override void Shoot()
        {
            timer = 0f;
            var shotgunData = weaponData as ShotgunData;
            if (shotgunData && !ammunition.IsMagazineEmpty())
            {
                var newBullet = Instantiate(ammunition.AmmunitionData.BulletData.BulletPrefab, transform.position + shotgunData.GripTransform.position, Quaternion.identity);
                var bulletRigidboy = newBullet.GetComponent<Rigidbody>();
                var bulletComponent = newBullet.GetComponent<Bullet>();

                bulletRigidboy.velocity = transform.forward * bulletComponent.BulletData.Velocity;
                ammunition.RemoveBulletFromMagazine();
            }
        }
    }
}
