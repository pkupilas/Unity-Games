using UnityEngine;

namespace Weapons.Guns.Shotgun
{
    [CreateAssetMenu(menuName = "Weapons/Shotgun")]
    public class ShotgunData : WeaponData
    {
        [SerializeField]
        private GameObject _ammoTypePrefab;
        public GameObject AmmoTypePrefab => _ammoTypePrefab;
    }
}