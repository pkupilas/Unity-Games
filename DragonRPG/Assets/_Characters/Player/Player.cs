using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using _Camera;
using _Characters.Abilities;
using _Characters.CommonScripts;
using _Characters.Enemies;
using _Characters.Weapons;
using Random = UnityEngine.Random;


namespace _Characters.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _baseDamage = 10f;
        [SerializeField] private Weapon _currentWeaponConfig;
        [SerializeField] private AnimatorOverrideController _animatorOverrideController;
        [SerializeField] private List<AbilityConfig> _specialAbilities;
        [Range(0f, 1f)] [SerializeField] private float _criticalHitChance;
        [SerializeField] private float _criticalHitMultiplayer;
        [SerializeField] private ParticleSystem _criticalHitParticles;

        private Enemy _currentEnemy;
        private CameraRaycaster _cameraRaycaster;
        private Animator _animator;
        private GameObject _weaponInHand;
        private Health _health;
        private float _lastHitTime;

        private const string AttackTrigger = "AttackTrigger";
        private const string AttackAnimationName = "DEAFAULT ATTACK";


        void Start()
        {
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
            RegisterForMouseClick();
            PutWeaponInHand(_currentWeaponConfig);
            SetWeaponAnimation();
            AttachAvailableAbilities();
        }

        void Update()
        {
            if (_health.CurrentHealth > Mathf.Epsilon)
            {
                ScanForUsedAbility();
            }
        }

        private void RegisterForMouseClick()
        {
            _cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            _cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
        }


        private void SetWeaponAnimation()
        {
            _animator.runtimeAnimatorController = _animatorOverrideController;
            _animatorOverrideController[AttackAnimationName] = _currentWeaponConfig.GetAttackAnimationClip();
        }

        private void AttachAvailableAbilities()
        {
            foreach (var specialAbility in _specialAbilities)
            {
                specialAbility.AttachAbilityTo(gameObject);
            }
        }

        private void ScanForUsedAbility()
        {
            for (int i = 1; i <= _specialAbilities.Count; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    AttemptSpecialAbility(i-1);
                }
            }
        }

        private GameObject RequestDominantHand()
        {
            var dominantHands = GetComponentsInChildren<DominantHand>();
            int dominantHandsCount = dominantHands.Length;

            Assert.AreNotEqual(0, dominantHandsCount, "No dominant hand for player.");
            Assert.IsFalse(dominantHandsCount > 1, "Multiple dominant hands for player.");

            return dominantHands[0].gameObject;
        }

        private void OnMouseOverEnemy(Enemy enemy)
        {
            _currentEnemy = enemy;
            if (Input.GetMouseButton(0) && IsTargetInRange(enemy.gameObject))
            {
                AttackTarget();
            }

            if (Input.GetMouseButtonDown(1))
            {
                AttemptSpecialAbility(2);
            }
        }

        private void AttemptSpecialAbility(int abilityIndex)
        {
            var energyComponent = GetComponent<Energy>();
            float energyCost = _specialAbilities[abilityIndex].EnergyCost;

            if (energyComponent.IsEnergyAvailable(energyCost))
            {
                energyComponent.ProcessEnergy(energyCost);
                var specialAbilityParams = new AbilityParams(_currentEnemy, _baseDamage);
                _specialAbilities[abilityIndex].UseAbility(specialAbilityParams);
            }
        }

        private bool IsTargetInRange(GameObject target)
        {
            var distanceToTarget = (target.transform.position - transform.position).magnitude;
            return distanceToTarget <= _currentWeaponConfig.MaxAttackRange;
        }

        private void AttackTarget()
        {
            if (Time.time - _lastHitTime > _currentWeaponConfig.AttackCooldown)
            {
                SetWeaponAnimation();
                _animator.SetTrigger(AttackTrigger);
                float calculatedDamage = CalculateDamage();
                _currentEnemy.GetComponent<Health>().TakeDamage(calculatedDamage);
                _lastHitTime = Time.time;
            }
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

        public void PutWeaponInHand(Weapon weapon)
        {
            _currentWeaponConfig = weapon;
            var weaponPrefab = _currentWeaponConfig.WeaponPrefab;
            var dominantHand = RequestDominantHand();

            Destroy(_weaponInHand);
            _weaponInHand = Instantiate(weaponPrefab, dominantHand.transform);
            _weaponInHand.transform.localPosition = _currentWeaponConfig.GripTransform.localPosition;
            _weaponInHand.transform.localRotation = _currentWeaponConfig.GripTransform.localRotation;
        }
    }
}
