using UnityEngine;
using Weapons.Bullets;

namespace Weapons.Guns.Rifle
{
    public class Rifle : Firearm
    {
        protected override void Shoot()
        {
            timer = 0f;
            var rifleData = weaponData as RifleData;
            if (rifleData && !ammunition.IsMagazineEmpty() && !ammunition.IsReloading)
            {
                ManageNewBullet();
            }
        }
    }
}
