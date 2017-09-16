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

        public void SetConfig(SelfHealConfig config)
        {
            AbilityConfig = config;
        }
    
        public override void Use(AbilityParams useParams)
        {
            var selfHealConfig = AbilityConfig as SelfHealConfig;
            _player.Heal(selfHealConfig.HealAmount);
            _audioSource.clip = selfHealConfig.AbilitySound;
            _audioSource.Play();
            PlayParticleEffect();
        }

        protected override void PlayParticleEffect()
        {
            var particlePrefab = AbilityConfig.ParticleEffect;
            var particles = Instantiate(particlePrefab, transform.position, particlePrefab.transform.rotation);
            particles.transform.parent = _player.transform;
            var particlesComponenet = particles.GetComponent<ParticleSystem>();
            particlesComponenet.Play();
            Destroy(particles, particlesComponenet.main.duration);
        }
    }
}
