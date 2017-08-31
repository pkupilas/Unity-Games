using UnityEngine;
using Weapons;

namespace Characters.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private WeaponData _weaponData;
        
        void Start()
        {
            EquipWeapon();
        }

        private void EquipWeapon()
        {
            var spawnedWeapon = Instantiate(_weaponData.WeaponPrefab, transform);

            spawnedWeapon.transform.localPosition = _weaponData.GripTransform.localPosition;
            spawnedWeapon.transform.localRotation = _weaponData.GripTransform.localRotation;
        }
    }
}