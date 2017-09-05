using UnityEngine;
using Weapons;

public class GunImage : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;

    public WeaponData WeaponData => _weaponData;
}
