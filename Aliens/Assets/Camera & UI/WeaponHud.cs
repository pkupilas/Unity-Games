using System;
using Assets.Weapons.Guns;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHud : MonoBehaviour
{
    private int _currentWeaponIndex;
    
    void Update()
    {
        HighlightCurrentWeapon();
        HandleScrollWheel();
    }

    private void HighlightCurrentWeapon()
    {
        foreach (Transform weaponImageTransform in transform)
        {
            var imageComponent = weaponImageTransform.GetComponent<Image>();
            if (imageComponent)
            {
                imageComponent.color = Color.black;
            }
        }

        transform.GetChild(_currentWeaponIndex).GetComponent<Image>().color = Color.red;
    }

    private void HandleScrollWheel()
    {
        var scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (Math.Abs(scrollWheel) < Mathf.Epsilon) return;

        var firearmWeapon = FindObjectOfType<Firearm>();
        if (firearmWeapon && firearmWeapon.Ammunition.IsReloading) return;

        _currentWeaponIndex = scrollWheel > 0 ? _currentWeaponIndex + 1 : _currentWeaponIndex - 1;
        _currentWeaponIndex = _currentWeaponIndex >= transform.childCount ? 0 : _currentWeaponIndex;
        _currentWeaponIndex = _currentWeaponIndex < 0 ? transform.childCount - 1 : _currentWeaponIndex;
    }

    public GameObject GetActiveWeapon()
    {
        return transform.GetChild(_currentWeaponIndex).gameObject;
    }

    public int GetActiveWeaponIndex()
    {
        return _currentWeaponIndex;
    }
}
