using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public EnemySpawnerSettings EnemySpawnerSettings;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private GameObject PickRandomEnemy()
    {
        var possibleEnemies = EnemySpawnerSettings.Enemies;
        int randomIndex = Random.Range(0, possibleEnemies.Count);

        return possibleEnemies[randomIndex];
    }

    private GameObject PickRandomSpawnPoint()
    {
        var possibleSpawnPoints = EnemySpawnerSettings.SpawnPoints;
        int randomIndex = Random.Range(0, possibleSpawnPoints.Count);

        return possibleSpawnPoints[randomIndex];
    }

    private IEnumerator SpawnEnemy()
    {
        var enemyToSpawn = PickRandomEnemy();
        var spawnPoint = PickRandomSpawnPoint();

        var enemy = Instantiate(enemyToSpawn, spawnPoint.transform.position, Quaternion.identity, transform);

        yield return new WaitForSeconds(EnemySpawnerSettings.SpawnDelay);

        StartCoroutine(SpawnEnemy());
    }

}
