using UnityEngine;
using Weapons.Bullets;

namespace Weapons.Guns.MachineGun
{
    public class MachineGun : Firearm
    {
        protected override void Shoot()
        {
            timer = 0f;
            var machineGunData = weaponData as MachineGunData;
            if (machineGunData && !ammunition.IsMagazineEmpty() && !ammunition.IsReloading)
            {
                ManageNewBullet();
            }
        }
    }
}
