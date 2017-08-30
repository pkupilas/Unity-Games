using System.Collections.Generic;
using UnityEngine;

namespace Characters.Enemies
{
    [CreateAssetMenu(menuName = "Characters/PossibleEnemies")]
    public class PossibleEnemies : ScriptableObject
    {
        [SerializeField] private List<EnemyData> _enemyList;

        public List<EnemyData> EnemyList => _enemyList;
    }
}