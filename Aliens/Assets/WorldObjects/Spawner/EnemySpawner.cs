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

        private int _enemyCounter;
        private int _enemiesPerWave = 1;
        private bool _isSpawning;
        private float _timeBetweenWaves = 10f;
        private float _timer;

        void Update()
        {
            if (!_isSpawning && _enemyCounter < _enemiesPerWave)
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
            _enemyCounter++;
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