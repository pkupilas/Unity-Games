using System.Collections.Generic;
using UnityEngine;

namespace WorldObjects.Spawner
{
    [CreateAssetMenu(menuName = "SpawnPoints")]
    public class PossibleSpawnPoints : ScriptableObject
    {
        [SerializeField] private List<GameObject> _spawnPoints;

        public List<GameObject> SpawnPoints => _spawnPoints;
    }
}
