using UnityEngine;

namespace _Characters.SpecialAbilities.PowerAttack
{
    public class PowerAttackBehaviour : MonoBehaviour, IAbility
    {
        private PowerAttackConfig _powerAttackConfig;
        private AudioSource _audioSource;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void SetConfig(PowerAttackConfig config)
        {
            _powerAttackConfig = config;
        }

        public void Use(AbilityParams useParams)
        {
            DealDamage(useParams);
            _audioSource.clip = _powerAttackConfig.AbilitySound;
            _audioSource.Play();
            PlayParticleEffect();
        }

        private void DealDamage(AbilityParams useParams)
        {
            float finalDamage = useParams.PlayerBaseDamage + _powerAttackConfig.GetExtraDamage();
            useParams.Target.TakeDamage(finalDamage);
        }

        private void PlayParticleEffect()
        {
            var particlePrefab = _powerAttackConfig.ParticleEffect;
            var particles = Instantiate(particlePrefab, transform.position, particlePrefab.transform.rotation);
            var particlesComponenet = particles.GetComponent<ParticleSystem>();
            particlesComponenet.Play();
            Destroy(particles, particlesComponenet.main.duration);
        }
    }
}
