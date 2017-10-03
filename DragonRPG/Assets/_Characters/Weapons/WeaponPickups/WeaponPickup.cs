using UnityEngine;
using _Characters.CommonScripts;

namespace _Characters.Weapons.WeaponPickups
{
    [ExecuteInEditMode]
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private AudioClip _pickUpSound;
        private AudioSource _audioSource;

        void Start ()
        {
            _audioSource = GetComponent<AudioSource>();
        }
	
        void Update ()
        {
            if (!Application.isPlaying)
            {
                DestroyAllChildren();
                InstantiateWeapon();
            }
        }

        private void DestroyAllChildren()
        {
            int childCount = transform.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        private void InstantiateWeapon()
        {
            var newWeapon = _weaponConfig.WeaponPrefab;
            newWeapon.transform.position = Vector3.zero;
            Instantiate(newWeapon, gameObject.transform);
        }

        private void OnTriggerEnter(Collider other)
        {
            FindObjectOfType<WeaponSystem>().PutWeaponInHand(_weaponConfig);
            _audioSource.PlayOneShot(_pickUpSound);
        }
    }
}
