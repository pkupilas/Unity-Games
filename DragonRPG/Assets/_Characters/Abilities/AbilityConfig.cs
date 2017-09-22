using UnityEngine;

namespace _Characters.Abilities
{
    public abstract class AbilityConfig : ScriptableObject
    {
        [Header("Special Ability General")]
        [SerializeField] private float _energyCost;
        [SerializeField] private GameObject _particleEffect;
        [SerializeField] private AudioClip _abilitySound;

        protected AbilityBehaviour _behaviour;

        protected abstract void SetBehaviourComponent(GameObject target);

        public void AttachAbilityTo(GameObject gameObject)
        {
            SetBehaviourComponent(gameObject);
            _behaviour.SetConfig(this);
        }

        public void UseAbility(GameObject target)
        {
            _behaviour.Use(target);
        }

        public float EnergyCost => _energyCost;

        public GameObject ParticleEffect => _particleEffect;

        public AudioClip AbilitySound => _abilitySound;
    }
}