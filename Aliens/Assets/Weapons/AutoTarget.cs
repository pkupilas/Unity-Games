using UnityEngine;

namespace Weapons
{
    public class AutoTarget : MonoBehaviour
    {
        private GameObject _spottedEnemy;
        private LayerMask _enemyMask;

        public GameObject SpottedEnemy => _spottedEnemy;

        void Start()
        {
            _enemyMask = LayerMask.GetMask("Enemy");
        }

        void Update()
        {
            LookForEnemy();
        }

        private void LookForEnemy()
        {
            float _camRayLength = 200f;
            RaycastHit cameraRayHitWithEnemy;
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            _spottedEnemy = Physics.Raycast(cameraRay, out cameraRayHitWithEnemy, _camRayLength, _enemyMask)
                            ? cameraRayHitWithEnemy.transform.gameObject
                            : null;
        }
    }
}
