using UnityEngine;

namespace _Characters.Abilities.PowerAttack
{
    public class PowerAttackBehaviour : AbilityBehaviour
    {
        private AudioSource _audioSource;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void SetConfig(PowerAttackConfig config)
        {
            AbilityConfig = config;
        }

        public override void Use(AbilityParams useParams)
        {
            DealDamage(useParams);
            _audioSource.clip = AbilityConfig.AbilitySound;
            _audioSource.Play();
            PlayParticleEffect();
        }

        private void DealDamage(AbilityParams useParams)
        {
            float finalDamage = useParams.PlayerBaseDamage + (AbilityConfig as PowerAttackConfig).GetExtraDamage();
            useParams.Target.TakeDamage(finalDamage);
        }

        protected override void PlayParticleEffect()
        {
            var particlePrefab = AbilityConfig.ParticleEffect;
            var particles = Instantiate(particlePrefab, transform.position, particlePrefab.transform.rotation);
            var particlesComponenet = particles.GetComponent<ParticleSystem>();
            particlesComponenet.Play();
            Destroy(particles, particlesComponenet.main.duration);
        }
    }
}
