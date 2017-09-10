using Characters.Player;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponData weaponData;

        protected Player player;
        protected float timer;
        protected AutoTarget autoTarget;
        private AudioSource _audioSource;

        public WeaponData WeaponData => weaponData;

        protected virtual void Start()
        {
            player = FindObjectOfType<Player>();
            timer = weaponData.AttackCooldown;
            autoTarget = GetComponent<AutoTarget>();
            _audioSource = GetComponent<AudioSource>();
        }

        protected virtual void Update()
        {
            timer += Time.deltaTime;
            if (Input.GetButton("Fire1") && timer >= weaponData.AttackCooldown)
            {
                Shoot();
            }
        }

        protected void PlayWeaponSound()
        {
            _audioSource.clip = weaponData.AudioClip;
            _audioSource.Play();
        }

        protected abstract void Shoot();
    }
}
