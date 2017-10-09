using UnityEngine;

namespace _Characters.Weapons
{
    [CreateAssetMenu(menuName = "RPG/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private GameObject _weaponPrefab;
        [SerializeField] private AnimationClip _attackAnimationClip;
        [SerializeField] private float _timeBetweenAnimationCycles = 0.5f;
        [SerializeField] private float _maxAttackRange = 2f;
        [SerializeField] private float _additionalDamage = 10f;
        [SerializeField] private Transform _gripTransform;
        [SerializeField] private float _damageDelay = 0.5f;
        
        public GameObject WeaponPrefab => _weaponPrefab;
        public float TimeBetweenAnimationCycles => _timeBetweenAnimationCycles;
        public float MaxAttackRange => _maxAttackRange;
        public float AdditionalDamage => _additionalDamage;
        public Transform GripTransform => _gripTransform;
        public float DamageDelay => _damageDelay;

        public AnimationClip GetAttackAnimationClip()
        {
            RemoveAnimationEvent();
            return _attackAnimationClip;
        }

        private void RemoveAnimationEvent()
        {
            _attackAnimationClip.events = new AnimationEvent[0];
        }
    }
}
