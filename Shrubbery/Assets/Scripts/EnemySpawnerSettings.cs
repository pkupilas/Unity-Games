using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemySpawner")]
public class EnemySpawnerSettings : ScriptableObject
{
    [SerializeField]
    private List<GameObject> _spawnPoints;
    [SerializeField]
    private List<GameObject> _enemies;
    [SerializeField]
    private float _spawnDelay;
    
    public List<GameObject> SpawnPoints => _spawnPoints;
    public List<GameObject> Enemies => _enemies;
    public float SpawnDelay => _spawnDelay;
}
