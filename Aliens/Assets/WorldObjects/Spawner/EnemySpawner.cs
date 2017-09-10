using System.Collections;
using Characters.Enemies;
using UnityEngine;

namespace WorldObjects.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private PossibleEnemies _possibleEnemies;
        [SerializeField] private PossibleSpawnPoints _possibleSpawnPoints;

        private int _enemiesPerWave = 5;
        private int _currentWave = 1;
        private int _killedInCurrentWave;
        private int _enemyCounter;
        private bool _isSpawning;
        private bool _isWaveBreak;
        private float _roundBreakTimer;
        private float _roundBreakRemainingTime;

        private const float RoundBreak = 10f;
        private const int EnemiesGrowth = 15;
        private const float SpawnCooldown = 1f;

        public bool IsWaveBreak => _isWaveBreak;
        public int CurrentWave => _currentWave;
        public int RemainingEnemyCount => _enemiesPerWave - _killedInCurrentWave;
        public float RoundBreakRemainingTime => _roundBreakRemainingTime;

        void Update()
        {
            if (!_isWaveBreak && !_isSpawning && _enemyCounter < _enemiesPerWave)
            {
                _isSpawning = true;
                StartCoroutine(SpawnEnemies());
            }

            if (_enemiesPerWave - _killedInCurrentWave == 0)
            {
                _isWaveBreak = true;
                ManageRoundBreak();
            }
        }

        private void ManageRoundBreak()
        {
            _roundBreakTimer += Time.deltaTime;
            _roundBreakRemainingTime = RoundBreak - _roundBreakTimer;
            if (_roundBreakTimer >= RoundBreak)
            {
                _isWaveBreak = false;
                _currentWave++;
                _enemyCounter = 0;
                _enemiesPerWave += EnemiesGrowth;
                _killedInCurrentWave = 0;
                _roundBreakTimer = 0;
            }
        }

        private IEnumerator SpawnEnemies()
        {
            yield return new WaitForSecondsRealtime(SpawnCooldown);

            var randomEnemy = GetRandomEnemy();
            var randomSpawnPoint = GetRandomSpawnPoint();
            Instantiate(randomEnemy, randomSpawnPoint, Quaternion.identity);
            _isSpawning = false;
            _enemyCounter++;
        }

        private GameObject GetRandomEnemy()
        {
            int randomIndex = Random.Range(0, _possibleEnemies.EnemyList.Count);
            return _possibleEnemies.EnemyList[randomIndex].EnemyPrefab;
        }

        private Vector3 GetRandomSpawnPoint()
        {
            var spawnPoints = _possibleSpawnPoints.SpawnPoints;
            int randomIndex = Random.Range(0, spawnPoints.Count);
            var randomPoint = spawnPoints[randomIndex];
            return randomPoint.transform.position;
        }

        public void IncNumberOfKilledInCurrentWave()
        {
            _killedInCurrentWave++;
        }
    }
}