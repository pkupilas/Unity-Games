using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using _Camera;
using _Characters.Enemies;
using _Characters.SpecialAbilities;
using _Core;
// TODO: Consider rewiring
using _Weapons;
using Random = UnityEngine.Random;


namespace _Characters
{
    public class Player : MonoBehaviour, IDamageable
    {

        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _baseDamage = 10f;
        [SerializeField] private Weapon _weaponInUse;
        [SerializeField] private AnimatorOverrideController _animatorOverrideController;
        [SerializeField] private List<SpecialAbilityConfig> _specialAbilities;
        [SerializeField] private List<AudioClip> _deathSounds;
        [SerializeField] private List<AudioClip> _takeDamageSounds;

        private Enemy _currentEnemy;
        private CameraRaycaster _cameraRaycaster;
        private Animator _animator;
        private AudioSource _audioSource;
        private bool _isDying;
        private float _currentHealth;
        private float _lastHitTime;

        private const string DeathTrigger = "DeathTrigger";
        private const string AttackTrigger = "AttackTrigger";
        private const string AttackAnimationName = "DEAFAULT ATTACK";


        void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            RegisterForMouseClick();
            SetCurrentHealthToMax();
            PutWeaponInHand();
            SetAnimator();
            AttachAvailableAbilities();
        }

        void Update()
        {
            if (_currentHealth > Mathf.Epsilon)
            {
                ScanForUsedAbility();
            }
        }

        private void RegisterForMouseClick()
        {
            _cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            _cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
        }

        private void SetCurrentHealthToMax()
        {
            _currentHealth = _maxHealth;
        }

        private void PutWeaponInHand()
        {
            var weaponPrefab = _weaponInUse.GetWeaponPrefab();
            var dominantHand = RequestDominantHand();
            var spawnedWeapon = Instantiate(weaponPrefab, dominantHand.transform);

            spawnedWeapon.transform.localPosition = _weaponInUse.gripTransform.localPosition;
            spawnedWeapon.transform.localRotation = _weaponInUse.gripTransform.localRotation;
        }

        private void SetAnimator()
        {
            _animator = GetComponent<Animator>();
            _animator.runtimeAnimatorController = _animatorOverrideController;
            _animatorOverrideController[AttackTrigger] = _weaponInUse.GetAttackAnimationClip();
        }

        private void AttachAvailableAbilities()
        {
            foreach (var specialAbility in _specialAbilities)
            {
                specialAbility.AttachComponentTo(gameObject);
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

        public void ChangeHealth(float damage)
        {
            if (_isDying) return;
            if (damage > 0)
            {
                PlaySound(GetRandomClipFrom(_takeDamageSounds));
            }
            else
            {
                //TODO: Play heal sound
            }

            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, _maxHealth);

            if (_currentHealth <= 0)
            {
                StartCoroutine(KillPlayer());
            }
        }

        private IEnumerator KillPlayer()
        {
            var deathClip = GetRandomClipFrom(_deathSounds);
            _isDying = true;
            PlaySound(deathClip);
            _animator.SetTrigger(DeathTrigger);
            yield return new WaitForSeconds(deathClip.length);

            SceneManager.LoadScene(0);
        }

        private void PlaySound(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
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
            float energyCost = _specialAbilities[abilityIndex].GetEnergyCost();

            if (energyComponent.IsEnergyAvailable(energyCost))
            {
                energyComponent.ProcessEnergy(energyCost);
                var specialAbilityParams = new SpecialAbilityParams(_currentEnemy, _baseDamage);
                _specialAbilities[abilityIndex].UseAbility(specialAbilityParams);
            }
        }

        private bool IsTargetInRange(GameObject target)
        {
            var distanceToTarget = (target.transform.position - transform.position).magnitude;
            return distanceToTarget <= _weaponInUse.GetMaxAttackRange();
        }

        private void AttackTarget()
        {
            if (Time.time - _lastHitTime > _weaponInUse.GetAttackCooldown())
            {
                _animator.SetTrigger(AttackTrigger);
                _currentEnemy.GetComponent<Enemy>().ChangeHealth(_baseDamage);
                _lastHitTime = Time.time;
            }
        }

        private AudioClip GetRandomClipFrom(List<AudioClip> sounds)
        {
            int randomIndex = Random.Range(0, sounds.Count);
            return sounds[randomIndex];
        }

        public float HealthAsPercentage => _currentHealth / _maxHealth;
    }
}
