using System.Collections;
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

        void Update()
        {
            bool isTargetDead;
            bool isTargetOutOfRange;
            bool isCharacterDead = !_character.GetComponent<Health>().IsAlive;

            if (_target == null)
            {
                isTargetDead = false;
                isTargetOutOfRange = false;
            }
            else
            {
                isTargetDead = !_target.GetComponent<Health>().IsAlive;
                isTargetOutOfRange = Vector3.Distance(_target.transform.position, transform.position) >
                                     _currentWeaponConfig.MaxAttackRange;
            }

            if (isCharacterDead || isTargetDead || isTargetOutOfRange)
            {
                StopAllCoroutines();
            }
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

        public void SetAndAttackTarget(GameObject targetToAttack)
        {
            _target = targetToAttack;
            StartCoroutine(AttackTarget());
        }

        public IEnumerator AttackTarget()
        {
            bool isAttackerAlive = GetComponent<Health>().IsAlive;
            bool isTargetAlive = _target.GetComponent<Health>().IsAlive;

            while (isAttackerAlive && isTargetAlive)
            {
                float animationClipTime = _currentWeaponConfig.GetAttackAnimationClip().length /
                                          _character.AnimationSpeedMultiplier;
                float timeToWait = animationClipTime +
                                   _currentWeaponConfig.TimeBetweenAnimationCycles * _character.AnimationSpeedMultiplier;

                if (Time.time - _lastHitTime > timeToWait)
                {
                    _lastHitTime = Time.time;
                    transform.LookAt(_target.transform);
                    SetWeaponAnimation();
                    _animator.SetTrigger(AttackTrigger);
                    StartCoroutine(DealDamageAfterDelay());
                }
                yield return new WaitForSeconds(timeToWait);
            }
        }

        private IEnumerator DealDamageAfterDelay()
        {
            yield return new WaitForSeconds(_currentWeaponConfig.DamageDelay);
            _target.GetComponent<Health>().TakeDamage(CalculateDamage());
        }

        public void StopAttacking()
        {
            StopAllCoroutines();
        }
    }
}
