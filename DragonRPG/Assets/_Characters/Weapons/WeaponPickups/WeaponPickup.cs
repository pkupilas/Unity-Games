using UnityEngine;

namespace _Characters.Weapons.WeaponPickups
{
    [ExecuteInEditMode]
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
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
            for (int i = childCount-1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        private void InstantiateWeapon()
        {
            var newWeapon = _weapon.WeaponPrefab;
            newWeapon.transform.position = Vector3.zero;
            Instantiate(newWeapon, gameObject.transform);
        }

        private void OnTriggerEnter(Collider other)
        {
            FindObjectOfType<Player.PlayerMovement>().PutWeaponInHand(_weapon);
            _audioSource.PlayOneShot(_pickUpSound);
        }
    }
}
