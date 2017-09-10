using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons.AmmoSpawner
{
    public class AmmoSpawner : MonoBehaviour
    {
        [SerializeField] private PossibleWeapons _possibleWeapons;
        [SerializeField] private AmmoSpawnerData _ammoSpawnerData;

        private AudioSource _audioSource;
        private float _timer;
        private float _cooldown;

        private const float MinCooldown = 10f;
        private const float MaxCooldown = 20f;

        void Start()
        {
            _cooldown = GenerateCooldown();
            SetAudioSource();
        }

        private void SetAudioSource()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = _ammoSpawnerData.PickupAudio;
        }

        private float GenerateCooldown()
        {
            return Random.Range(MinCooldown, MaxCooldown);
        }

        void Update ()
        {
            if (IsWeaponSlotEmpty())
            {
                _timer += Time.deltaTime;
                if (!(_timer >= _cooldown)) return;

                _timer = 0;
                _cooldown = GenerateCooldown();

                var randomWeapon = PickRandomWeapon();
                var newPosition = transform.position + Vector3.up;
                var spawnedAmmoPack = Instantiate(_ammoSpawnerData.AmmoPackPrefab, newPosition, Quaternion.identity);
                var ammoPackGrip = _ammoSpawnerData.AmmoPackGrip;

                spawnedAmmoPack.transform.parent = gameObject.transform;
                spawnedAmmoPack.transform.localPosition = ammoPackGrip.localPosition;
                spawnedAmmoPack.transform.localRotation = ammoPackGrip.localRotation;
                spawnedAmmoPack.transform.localScale = ammoPackGrip.localScale;
                spawnedAmmoPack.GetComponent<AmmoPack>().WeaponInAmmoPack = randomWeapon;
            }
        }

        private GameObject PickRandomWeapon()
        {
            var weaponList = _possibleWeapons.WeaponList;
            int randomIndex = Random.Range(0, weaponList.Count);
            return weaponList[randomIndex].WeaponPrefab;
        }

        private bool IsWeaponSlotEmpty()
        {
            return transform.childCount == 0;
        }

        public void PlayPickUpSound()
        {
            _audioSource.Play();
        }
    }
}
