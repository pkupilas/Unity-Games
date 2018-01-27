using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public EnemySpawnerSettings enemySpawnerSettings;
    private List<GameObject> _spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private GameObject PickRandomEnemy()
    {
        var possibleEnemies = enemySpawnerSettings.Enemies;
        int randomIndex = Random.Range(0, possibleEnemies.Count);

        return possibleEnemies[randomIndex];
    }

    private GameObject PickRandomSpawnPoint()
    {
        var possibleSpawnPoints = enemySpawnerSettings.SpawnPoints;
        int randomIndex = Random.Range(0, possibleSpawnPoints.Count);

        return possibleSpawnPoints[randomIndex];
    }

    private IEnumerator SpawnEnemy()
    {
        var enemyToSpawn = PickRandomEnemy();
        var spawnPoint = PickRandomSpawnPoint();

        var enemy = Instantiate(enemyToSpawn, spawnPoint.transform.position, Quaternion.identity, transform);
        _spawnedEnemies.Add(enemy);

        yield return new WaitForSeconds(enemySpawnerSettings.SpawnDelay);

        StartCoroutine(SpawnEnemy());
    }

}
