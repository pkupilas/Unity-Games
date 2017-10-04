using System.Collections;
using UnityEngine;
using _Camera;
using _Characters.CommonScripts;
using _Characters.Enemies;

namespace _Characters.Player
{
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(WeaponSystem))]
    [RequireComponent(typeof(SpecialAbilities))]
    public class PlayerControl : MonoBehaviour
    {
        private Character _character;
        private Health _health;
        private WeaponSystem _weaponSystem;
        private SpecialAbilities _specialAbilities;
        private int _powerAttackAbilityIndex = 2;

        void Start()
        {
            _character = GetComponent<Character>();
            _health = GetComponent<Health>();
            _weaponSystem = GetComponent<WeaponSystem>();
            _specialAbilities = GetComponent<SpecialAbilities>();
            RegisterForMouseClick();
        }

        void Update()
        {
            if (_health.IsAlive)
            {
                ScanForUsedAbility();
            }
        }

        private void RegisterForMouseClick()
        {
            var cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
            cameraRaycaster.onMouseOverTerrain += OnMouseOverTerrain;
        }

        private void OnMouseOverEnemy(EnemyAI enemyAi)
        {
            if (Input.GetMouseButton(0) && IsTargetInRange(enemyAi.gameObject))
            {
                _weaponSystem.SetAndAttackTarget(enemyAi.gameObject);
            }
            if (Input.GetMouseButton(0) && !IsTargetInRange(enemyAi.gameObject))
            {
                StartCoroutine(MoveAndAttack(enemyAi.gameObject));
            }
            if (Input.GetMouseButtonDown(1) && IsTargetInRange(enemyAi.gameObject))
            {
                _powerAttackAbilityIndex = 2;
                _specialAbilities.AttemptSpecialAbility(_powerAttackAbilityIndex, enemyAi.gameObject);
            }
            if (Input.GetMouseButton(1) && !IsTargetInRange(enemyAi.gameObject))
            {
                StartCoroutine(MoveAndPowerAttack(enemyAi.gameObject));
            }
        }

        private IEnumerator MoveAndAttack(GameObject target)
        {
            yield return StartCoroutine(MoveToTarget(target));
            _weaponSystem.SetAndAttackTarget(target);
        }

        private IEnumerator MoveAndPowerAttack(GameObject target)
        {
            yield return StartCoroutine(MoveToTarget(target));
            _specialAbilities.AttemptSpecialAbility(_powerAttackAbilityIndex, target?.gameObject);
        }

        private IEnumerator MoveToTarget(GameObject target)
        {
            _character.SetDestination(target.transform.position);
            while (!IsTargetInRange(target))
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }

        private void OnMouseOverTerrain(Vector3 destination)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _character.SetDestination(destination);
            }
        }

        private void ScanForUsedAbility()
        {
            for (int i = 1; i <= _specialAbilities.SpecialAbilitiesCount; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    _specialAbilities.AttemptSpecialAbility(i - 1);
                }
            }
        }
        private bool IsTargetInRange(GameObject target)
        {
            var distanceToTarget = (target.transform.position - transform.position).magnitude;
            return distanceToTarget <= _weaponSystem.CurrentWeaponConfig.MaxAttackRange;
        }
    }
}
