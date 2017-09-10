using Characters.Player;
using UnityEngine;

namespace Weapons.AmmoSpawner
{
    public class AmmoPack : MonoBehaviour
    {
        public GameObject WeaponInAmmoPack { get; set; }
        
        void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<Player>();
            if (player)
            {
                var weaponBarrel = WeaponInAmmoPack.transform.GetChild(0).gameObject;
                var ammoSpawner = transform.parent.gameObject.GetComponent<AmmoSpawner>();

                player.AddMagazine(weaponBarrel);
                ammoSpawner.PlayPickUpSound();
                Destroy(gameObject);
            }
        }
    }
}
