using UnityEngine;
using _Camera;
using _Characters.CommonScripts;
using _Characters.Enemies;

namespace _Characters.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private EnemyAI _currentEnemyAi;
        private CameraRaycaster _cameraRaycaster;
        private Health _health;
        private SpecialAbilities _specialAbilities;
        private Character _character;
        private WeaponSystem _weaponSystem;

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
            _cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            _cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
            _cameraRaycaster.onMouseOverTerrain += OnMouseOverTerrain;
        }

        private void OnMouseOverEnemy(EnemyAI enemyAi)
        {
            _currentEnemyAi = enemyAi;
            if (Input.GetMouseButton(0) && IsTargetInRange(enemyAi.gameObject))
            {
                _weaponSystem.SetAndAttackTarget(_currentEnemyAi.gameObject);
            }

            if (Input.GetMouseButtonDown(1))
            {
                _specialAbilities.AttemptSpecialAbility(2, _currentEnemyAi?.gameObject);
            }
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
                    _specialAbilities.AttemptSpecialAbility(i - 1, _currentEnemyAi?.gameObject);
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
