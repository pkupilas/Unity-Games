using System.Collections.Generic;
using UnityEngine;

namespace Characters.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject _weaponHud;

        private WeaponData _currentWeaponData;
        private List<GameObject> _avaliableWeapons;
        private GameObject _activeWeapon;


        void Start()
        {
            _avaliableWeapons = new List<GameObject>();
            AddAllWeapons();
            SetCurrentWeaponData();
            EquipWeapon();
        }
        void Update()
        {
            SetCurrentWeaponData();
            UpdateWeapon();
        }

        private void AddAllWeapons()
        {
            var allWeaponDatas = _weaponHud.transform;
            foreach (Transform weaponDataTransform in allWeaponDatas)
            {
                var gunImageComponent = weaponDataTransform.GetComponent<GunImage>();
                if (gunImageComponent)
                {
                    var weaponData = gunImageComponent.WeaponData;
                    var newWeapon = Instantiate(weaponData.WeaponPrefab, transform);
                    newWeapon.transform.localPosition = weaponData.GripTransform.localPosition;
                    newWeapon.transform.localRotation = weaponData.GripTransform.localRotation;
                    _avaliableWeapons.Add(newWeapon);
                }
            }
        }
        
        private void SetCurrentWeaponData()
        {
            var weaponHudComponent = _weaponHud.GetComponent<WeaponHud>();
            var gunImageComponent = weaponHudComponent.GetActiveWeapon().GetComponent<GunImage>();
            _currentWeaponData = gunImageComponent.WeaponData;
        }

        private void UpdateWeapon()
        {
            var currentWeapon = GetComponentInChildren<Weapon>();
            if (currentWeapon)
            {
                if (currentWeapon.WeaponData != _currentWeaponData)
                {
                    EquipWeapon();
                }
            }
        }
        
        private void EquipWeapon()
        {
            foreach (Transform weapon in transform)
            {
                weapon.gameObject.SetActive(false);
            }
            int currentWeaponIndex = _weaponHud.GetComponent<WeaponHud>().GetActiveWeaponIndex();
            _activeWeapon = _avaliableWeapons[currentWeaponIndex];
            _activeWeapon.SetActive(true);
        }
    }
}