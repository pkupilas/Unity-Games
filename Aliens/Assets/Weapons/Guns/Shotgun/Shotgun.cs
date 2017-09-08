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

                if (autoTarget.SpottedEnemy)
                {
                    var pointOnEnemyHeight = autoTarget.SpottedEnemy.GetComponent<CapsuleCollider>().height / 2;
                    var targetVector = new Vector3(0f, pointOnEnemyHeight, 0f);
                    var tmp = autoTarget.SpottedEnemy.transform.position + targetVector - transform.position;
                    bulletRigidboy.velocity = tmp.normalized * bulletComponent.BulletData.Velocity;
                }
                else
                {
                    bulletRigidboy.velocity = transform.forward * bulletComponent.BulletData.Velocity;
                }
                ammunition.RemoveBulletFromMagazine();
            }
        }
    }
}
