using UnityEngine;

namespace Weapons.AmmoSpawner
{
    [CreateAssetMenu(menuName = "Armory/AmmoSpawner")]
    public class AmmoSpawnerData : ScriptableObject
    {
        [SerializeField] private GameObject _ammoPackPrefab;
        [SerializeField] private Transform _ammoPackGrip;
        [SerializeField] private AudioClip _pickupAudio;

        public GameObject AmmoPackPrefab => _ammoPackPrefab;
        public Transform AmmoPackGrip => _ammoPackGrip;
        public AudioClip PickupAudio => _pickupAudio;
    }
}
