using UnityEngine;

namespace Characters.Enemies
{
    public class EnemyUI : MonoBehaviour
    {
        
        [Tooltip("The UI canvas prefab")]
        [SerializeField] private GameObject _enemyCanvasPrefab = null;

        private Camera _cameraToLookAt;
        
        void Start()
        {
            _cameraToLookAt = Camera.main;
            Instantiate(_enemyCanvasPrefab, transform.position, Quaternion.identity, transform);
        }
        
        void LateUpdate()
        {
            transform.LookAt(_cameraToLookAt.transform);
        }
    }
}