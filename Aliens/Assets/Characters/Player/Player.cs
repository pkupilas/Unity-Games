using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Weapons;

namespace Characters.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private WeaponData _weaponData;
        private float _currentHealth;
        private const float MaxHealth = 100; 
        private float _speed = 10f;

        public float CurrentHealth => _currentHealth;

        void Start()
        {
            EquipWeapon();
            SetCurrentHealth();
        }

        void Update()
        {
            LookAtCursor();
            Move();
            Shoot();
        }

        private void SetCurrentHealth()
        {
            _currentHealth = MaxHealth;
        }

        private void EquipWeapon()
        {
            var spawnedWeapon = Instantiate(_weaponData.WeaponPrefab, transform);

            spawnedWeapon.transform.localPosition = _weaponData.GripTransform.localPosition;
            spawnedWeapon.transform.localRotation = _weaponData.GripTransform.localRotation;

        }

        private void Shoot()
        {
            if (CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                GetWeapon().GetComponent<Weapon>().Shoot();
            }
        }

        private GameObject GetWeapon()
        {
            foreach (Transform playerEqItem in transform)
            {
                var weaponComponent = playerEqItem.GetComponent<Weapon>();
                if (weaponComponent)
                {
                    return playerEqItem.gameObject;
                }
            }

            return null;
        }

        private void Move()
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

            transform.position += Vector3.forward * v * Time.deltaTime * _speed + Vector3.right * h * Time.deltaTime * _speed;
        }

        private void LookAtCursor()
        {
            RaycastHit cameraRayHit;
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cameraRay, out cameraRayHit))
            {
                    var targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                    transform.LookAt(targetPosition);
            }
        }

        public void TakeDamage(float damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, MaxHealth);
        }

    }
}
