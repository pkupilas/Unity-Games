using UnityEngine;

namespace _Characters.Abilities.SelfHeal
{
    public class SelfHealBehaviour : AbilityBehaviour
    {
        private Player _player;
        private AudioSource _audioSource;

        void Start()
        {
            _player = GetComponent<Player>();
            _audioSource = GetComponent<AudioSource>();
        }

        public override void Use(AbilityParams useParams)
        {
            var selfHealConfig = _abilityConfig as SelfHealConfig;
            _player.Heal(selfHealConfig.HealAmount);
            _audioSource.clip = selfHealConfig.AbilitySound;
            _audioSource.Play();
            PlayParticleEffect();
        }
    }
}
