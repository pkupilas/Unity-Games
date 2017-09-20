using UnityEngine;

// Add a UI Socket transform to your enemy
// Attach this script to the socket
// Link to a canvas prefab that contains NPC UI
namespace _Characters.Enemies
{
    public class EnemyUI : MonoBehaviour {

        // Works around Unity 5.5's lack of nested prefabs
        [Tooltip("The UI canvas prefab")] [SerializeField] private GameObject _enemyCanvasPrefab;

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