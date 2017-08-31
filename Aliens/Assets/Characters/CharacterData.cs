using UnityEngine;

namespace Characters
{
    public abstract class CharacterData : ScriptableObject
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _speed;

        public float MaxHealth => _maxHealth;
        public float Speed => _speed;
    }
}
