using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Weapons;
using Weapons.Rifle;

namespace Characters.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private WeaponData _weaponData;

        private float _speed = 10f;

        void Start()
        {
            EquipWeapon();
        }

        private void EquipWeapon()
        {
            var spawnedWeapon = Instantiate(_weaponData.WeaponPrefab, transform);

            spawnedWeapon.transform.localPosition = _weaponData.GripTransform.localPosition;
            spawnedWeapon.transform.localRotation = _weaponData.GripTransform.localRotation;

        }

        void Update()
        {
            LookAtCursor();
            Move();
            Shoot();
        }

        private void Shoot()
        {
            if (CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                GetWeapon().GetComponent<Rifle>().Shoot();
            }
        }

        private GameObject GetWeapon()
        {
            foreach (Transform playerEqItem in transform)
            {
                var rifleComponent = playerEqItem.GetComponent<Rifle>();
                if (rifleComponent)
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

            transform.position += transform.forward * v * Time.deltaTime * _speed + transform.right * h * Time.deltaTime * _speed;
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
    }
}
