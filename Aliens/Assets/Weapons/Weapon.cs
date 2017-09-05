using Characters.Player;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponData weaponData;

        public WeaponData WeaponData => weaponData;

        protected Player player;
        protected float timer;

        protected virtual void Start()
        {
            player = FindObjectOfType<Player>();
            timer = weaponData.AttackCooldown;
        }

        protected virtual void Update()
        {
            timer += Time.deltaTime;
            if (CrossPlatformInputManager.GetButtonDown("Fire1") && timer >= weaponData.AttackCooldown)
            {
                Shoot();
            }
        }

        protected abstract void Shoot();
    }
}