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
        private int _enemiesPerWave = 5;
        private bool _isSpawning;
        private float _timeBetweenWaves = 10f;
        private float _timer;
        private int _currentWave = 1;

        public int CurrentWave => _currentWave;
        public int RemainingEnemyCount => _enemiesPerWave - _enemyCounter;
        public int EnemiesPerWave => _enemiesPerWave;

        public int killedInCurrentWave;

        void Update()
        {
            if (!_isSpawning && _enemyCounter < _enemiesPerWave)
            {
                _isSpawning = true;
                StartCoroutine(SpawnEnemies());
            }

            if (_enemiesPerWave - killedInCurrentWave == 0)
            {
                _currentWave++;
                _enemyCounter = 0;
                _enemiesPerWave += 5;
                killedInCurrentWave = 0;
                _spawnCooldown -= 0.5f;
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