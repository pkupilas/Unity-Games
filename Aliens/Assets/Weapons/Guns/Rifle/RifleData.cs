using UnityEngine;

namespace Weapons.Guns.Rifle
{
    [CreateAssetMenu(menuName = "Weapons/Rifle")]
    public class RifleData : WeaponData
    {
        [SerializeField] private GameObject _ammoTypePrefab;
        public GameObject AmmoTypePrefab => _ammoTypePrefab;
    }
}