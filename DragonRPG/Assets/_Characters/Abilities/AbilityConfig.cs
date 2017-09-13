using UnityEngine;

namespace _Characters.SpecialAbilities
{
    public abstract class AbilityConfig : ScriptableObject
    {
        [Header("Special Ability General")]
        [SerializeField] private float _energyCost;
        [SerializeField] private GameObject _particleEffect;
        [SerializeField] private AudioClip _abilitySound;

        protected IAbility behaviour;

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