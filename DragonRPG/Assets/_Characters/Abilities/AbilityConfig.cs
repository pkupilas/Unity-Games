using UnityEngine;

namespace _Characters.Abilities
{
    public abstract class AbilityConfig : ScriptableObject
    {
        [Header("Special Ability General")]
        [SerializeField] private float _energyCost;
        [SerializeField] private GameObject _particleEffect;
        [SerializeField] private AudioClip _abilitySound;

        protected AbilityBehaviour behaviour;

        public abstract void AttachComponentTo(GameObject gameObject);

        public void UseAbility(AbilityParams useParams)
        {
            behaviour.Use(useParams);
        }

        public float EnergyCost => _energyCost;

        public GameObject ParticleEffect => _particleEffect;

        public AudioClip AbilitySound => _abilitySound;
    }
}