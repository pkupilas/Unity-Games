using UnityEngine;
using UnityEngine.Assertions;
using _Characters.Weapons;

namespace _Characters.CommonScripts
{
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField] private float _baseDamage = 10f;
        [Range(0f, 1f)] [SerializeField] private float _criticalHitChance;
        [SerializeField] private WeaponConfig _currentWeaponConfig;
        [SerializeField] private float _criticalHitMultiplayer;
        [SerializeField] private ParticleSystem _criticalHitParticles;

        private GameObject _weaponInHand;
        private GameObject _target;
        private Animator _animator;
        private Character _character;
        private float _lastHitTime;

        private const string AttackTrigger = "AttackTrigger";
        private const string AttackAnimationName = "DEAFAULT ATTACK";

        public WeaponConfig CurrentWeaponConfig => _currentWeaponConfig;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _character = GetComponent<Character>();
            PutWeaponInHand(_currentWeaponConfig);
            SetWeaponAnimation();
        }
        
        public void PutWeaponInHand(WeaponConfig weaponConfig)
        {
            _currentWeaponConfig = weaponConfig;
            var weaponPrefab = _currentWeaponConfig.WeaponPrefab;
            var dominantHand = RequestDominantHand();

            Destroy(_weaponInHand);
            _weaponInHand = Instantiate(weaponPrefab, dominantHand.transform);
            _weaponInHand.transform.localPosition = _currentWeaponConfig.GripTransform.localPosition;
            _weaponInHand.transform.localRotation = _currentWeaponConfig.GripTransform.localRotation;
        }
        
        private GameObject RequestDominantHand()
        {
            var dominantHands = GetComponentsInChildren<DominantHand>();
            int dominantHandsCount = dominantHands.Length;

            Assert.AreNotEqual(0, dominantHandsCount, "No dominant hand for player.");
            Assert.IsFalse(dominantHandsCount > 1, "Multiple dominant hands for player.");

            return dominantHands[0].gameObject;
        }

        private void SetWeaponAnimation()
        {
            var animatorOverrideController = _character.AnimatorOverrideController;
            _animator.runtimeAnimatorController = animatorOverrideController;
            animatorOverrideController[AttackAnimationName] = _currentWeaponConfig.GetAttackAnimationClip();
        }

        private float CalculateDamage()
        {
            float outputDamage = _baseDamage + _currentWeaponConfig.AdditionalDamage;
            bool isCriticalHit = Random.Range(0f, 1f) <= _criticalHitChance;

            if (isCriticalHit)
            {
                outputDamage *= _criticalHitMultiplayer;
                _criticalHitParticles.Play();
            }

            return outputDamage;
        }

        public void AttackTarget()
        {
            if (Time.time - _lastHitTime > _currentWeaponConfig.AttackCooldown)
            {
                SetWeaponAnimation();
                _animator.SetTrigger(AttackTrigger);
                float calculatedDamage = CalculateDamage();
                _target.GetComponent<Health>().TakeDamage(calculatedDamage);
                _lastHitTime = Time.time;
            }
        }

        public void SetAndAttackTarget(GameObject attackTarget)
        {
            _target = attackTarget;
            AttackTarget(); // TODO: Change to coroutine
        }
    }
}
