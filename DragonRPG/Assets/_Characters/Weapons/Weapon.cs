using UnityEngine;

namespace _Characters.Weapons
{
    [CreateAssetMenu(menuName = "RPG/Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private GameObject _weaponPrefab;
        [SerializeField] private AnimationClip _attackAnimationClip;
        [SerializeField] private float _attackCooldown = 0.5f;
        [SerializeField] private float _maxAttackRange = 2f;
        [SerializeField] private float _additionalDamage = 10f;
        [SerializeField] private Transform _gripTransform;
        
        public GameObject WeaponPrefab => _weaponPrefab;
        public float AttackCooldown => _attackCooldown;
        public float MaxAttackRange => _maxAttackRange;
        public float AdditionalDamage => _additionalDamage;
        public Transform GripTransform => _gripTransform;

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
