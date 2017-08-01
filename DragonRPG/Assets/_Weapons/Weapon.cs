using UnityEngine;

namespace _Weapons
{
    [CreateAssetMenu(menuName = "RPG/Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private GameObject _weaponPrefab;
        [SerializeField] private AnimationClip _attackAnimationClip;
        [SerializeField] private float _attackCooldown = 0.5f;
        [SerializeField] private float _maxAttackRange = 2f;

        public Transform gripTransform;

        public GameObject GetWeaponPrefab()
        {
            return _weaponPrefab;
        }
        public float GetAttackCooldown()
        {
            return _attackCooldown;
        }
        public float  GetMaxAttackRange()
        {
            return _maxAttackRange;
        }

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
