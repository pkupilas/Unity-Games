using Characters.Enemies;
using Characters.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    private Player _player;
    private AICharacterControl _aiCharacterControl;
    private NavMeshAgent _navMeshAgent;
    private float _currentHealth;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _aiCharacterControl = GetComponent<AICharacterControl>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        SetEnemyValues();
    }

    void Update()
    {
        _aiCharacterControl.SetTarget(_player.transform);
    }

    private void SetEnemyValues()
    {
        _navMeshAgent.speed = _enemyData.Speed;
        _currentHealth = _enemyData.Health;
    }
}