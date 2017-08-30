using System.Collections;
using System.Collections.Generic;
using Characters.Enemies;
using UnityEngine;

namespace WorldObjects.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private PossibleEnemies _possibleEnemies;
        [SerializeField] private List<GameObject> _spawnPoints;
        [SerializeField] private float _spawnCooldown;

        private bool _isSpawning;

        void Update()
        {
            if (!_isSpawning)
            {
                _isSpawning = true;
                StartCoroutine(SpawnEnemies());
            }
        }

        private IEnumerator SpawnEnemies()
        {
            yield return new WaitForSecondsRealtime(_spawnCooldown);

            var randomEnemy = GetRandomEnemy();
            var randomSpawnPoint = GetRandomSpawnPoint();
            Instantiate(randomEnemy, randomSpawnPoint.transform.position, Quaternion.identity);
            _isSpawning = false;
        }

        private GameObject GetRandomEnemy()
        {
            int randomIndex = Random.Range(0, _possibleEnemies.EnemyList.Count);
            return _possibleEnemies.EnemyList[randomIndex].EnemyPrefab;
        }

        private GameObject GetRandomSpawnPoint()
        {
            int randomIndex = Random.Range(0, _spawnPoints.Count);
            return _spawnPoints[randomIndex];
        }
    }
}