using UnityEngine;
using Weapons.Bullets;

namespace Weapons.Guns.Shotgun
{
    public class Shotgun : Firearm
    {
        protected override void Shoot()
        {
            timer = 0f;
            var shotgunData = weaponData as ShotgunData;
            if (shotgunData && !ammunition.IsMagazineEmpty() && !ammunition.IsReloading)
            {
                ManageNewBullet();
            }
        }
    }
}
