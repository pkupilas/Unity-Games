using UnityEngine;
using UnityEngine.Assertions;
using _Camera;
using _Core;
// TODO: Consider rewiring
using _Levels;
using _Weapons;


namespace _Characters
{
    public class Player : MonoBehaviour, IDamageable
    {

        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _damage = 10f;
        [SerializeField] private float _attackCooldown = 0.5f;
        [SerializeField] private float _maxAttackRange = 2f;
        [SerializeField] private Weapon _weaponInUse;
        [SerializeField] private AnimatorOverrideController _animatorOverrideController;

        private float _currentHealth;
        private float _lastHitTime;
        private GameObject _currentTarget;
        private CameraRaycaster _cameraRaycaster;


        void Start()
        {
            RegisterForMouseClick();
            _currentHealth = _maxHealth;
            PutWeaponInHand();
            SetAnimatorController();
        }

        private void SetAnimatorController()
        {
            var animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = _animatorOverrideController;
            _animatorOverrideController["DEAFAULT ATTACK"] = _weaponInUse.GetAttackAnimationClip();
        }

        private void PutWeaponInHand()
        {
            var weaponPrefab = _weaponInUse.GetWeaponPrefab();
            var dominantHand = RequestDominantHand();
            var spawnedWeapon = Instantiate(weaponPrefab, dominantHand.transform);

            spawnedWeapon.transform.localPosition = _weaponInUse.gripTransform.localPosition;
            spawnedWeapon.transform.localRotation = _weaponInUse.gripTransform.localRotation;
        }

        private GameObject RequestDominantHand()
        {
            var dominantHands = GetComponentsInChildren<DominantHand>();
            int dominantHandsCount = dominantHands.Length;

            Assert.AreNotEqual(0, dominantHandsCount, "No dominant hand for player.");
            Assert.IsFalse(dominantHandsCount > 1, "Multiple dominant hands for player.");

            return dominantHands[0].gameObject;
        }

        private void RegisterForMouseClick()
        {
            _cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            _cameraRaycaster.notifyMouseClickObservers += OnMouseClicked;
        }

        private void OnMouseClicked(RaycastHit raycastHit, int layerHit)
        {
            if (layerHit == Utilities.EnemyLayerNumber)
            {
                var enemy = raycastHit.collider.gameObject;

                if ((enemy.transform.position - transform.position).magnitude > _maxAttackRange)
                {
                    return;
                }

                _currentTarget = enemy;

                if (Time.time - _lastHitTime > _attackCooldown)
                {
                    _currentTarget.GetComponent<Enemy>().TakeDamage(_damage);
                    _lastHitTime = Time.time;
                }
            }
        }

        public float HealthAsPercentage
        {
            get { return _currentHealth / _maxHealth; }
        }

        public void TakeDamage(float damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, _maxHealth);
        }
    }
}
