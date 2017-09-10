using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(menuName = "Weapons/PossibleWeapons")]
    public class PossibleWeapons : ScriptableObject
    {
        [SerializeField] private List<WeaponData> _weaponList;

        public List<WeaponData> WeaponList => _weaponList;
    }
}
