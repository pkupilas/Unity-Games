using UnityEngine;
using Weapons;

namespace Characters.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject _weaponHud;

        private WeaponData _weaponData;
        private GameObject _equippedWeapon;


        void Start()
        {
            SetCurrentWeaponData();
            EquipWeapon();
        }

        void Update()
        {
            SetCurrentWeaponData();
            UpdateWeapon();
        }

        private void SetCurrentWeaponData()
        {
            var weaponHudComponent = _weaponHud.GetComponent<WeaponHud>();
            var gunImageComponent = weaponHudComponent.GetActiveWeapon().GetComponent<GunImage>();
            _weaponData = gunImageComponent.WeaponData;
        }

        private void UpdateWeapon()
        {
            var currentWeapon = GetComponentInChildren<Weapons.Weapon>();
            if (currentWeapon)
            {
                if (currentWeapon.WeaponData != _weaponData)
                {
                    Destroy(_equippedWeapon);
                    EquipWeapon();
                }
            }
        }
        
        private void EquipWeapon()
        {
            _equippedWeapon = Instantiate(_weaponData.WeaponPrefab, transform);

            _equippedWeapon.transform.localPosition = _weaponData.GripTransform.localPosition;
            _equippedWeapon.transform.localRotation = _weaponData.GripTransform.localRotation;
        }
    }
}