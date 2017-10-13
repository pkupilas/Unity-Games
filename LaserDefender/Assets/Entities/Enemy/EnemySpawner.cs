using UnityEngine;

namespace Entities.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemy;
        [SerializeField] private float _velocity = 15f;
        [SerializeField] private float _width = 10f;
        [SerializeField] private float _height = 5f;
        [SerializeField] private float _spawnDelay = 0.5f;

        private Moving _movingDirection = Moving.Stop;
        private float _xmin;
        private float _xmax;

        private enum Moving
        {
            Left,
            Right,
            Stop
        }
        
        void Start ()
        {
            float distance = transform.position.z - Camera.main.transform.position.z;
            var leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
            var rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distance));

            SpawnUntillFull();

            _xmin = leftBoundary.x;
            _xmax = rightBoundary.x;

        }

        void Update()
        {
            transform.position += (_movingDirection == Moving.Right ? Vector3.right : Vector3.left) * _velocity * Time.deltaTime;
            CheckChangeOfMovingDirection();
            if (CheckIfAllEnemiesAreDead())
            {
                SpawnUntillFull();
            }
        }

        private void SpawnEnemies()
        {
            foreach (Transform childTransform in transform)
            {
                var enemyInstance = Instantiate(_enemy, childTransform.position, Quaternion.identity);
                enemyInstance.transform.parent = childTransform;
            }
        }

        //TODO: Change to coroutine
        private void SpawnUntillFull()
        {
            var freePosition = GetNextFreeEnemyPosition();
            if (freePosition)
            {
                var enemyInstance = Instantiate(_enemy, freePosition.position, Quaternion.identity);
                enemyInstance.transform.parent = freePosition;
            }
            if (GetNextFreeEnemyPosition() != null)
            {
                Invoke("SpawnUntillFull", _spawnDelay);
            }
        }

        private Transform GetNextFreeEnemyPosition()
        {
            foreach (Transform childPosition in transform)
            {
                if (childPosition.childCount == 0)
                {
                    return childPosition;
                }
            }

            return null;
        }

        private bool CheckIfAllEnemiesAreDead()
        {
            foreach (Transform childPosition in transform)
            {
                if (childPosition.childCount > 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void CheckChangeOfMovingDirection()
        {
            float leftEdgeOfFormation = transform.position.x - (0.5f * _width);
            float rightEdgeOfFormation = transform.position.x + (0.5f * _width);
            
            if (rightEdgeOfFormation > _xmax)
            {
                _movingDirection = Moving.Left;
            }
            else if (leftEdgeOfFormation < _xmin)
            {
                _movingDirection = Moving.Right;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(_width, _height, 0));
        }
    }
}
