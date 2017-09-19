using UnityEngine;
namespace _Characters.Abilities
{
    public abstract class AbilityBehaviour : MonoBehaviour
    {
        protected AbilityConfig _abilityConfig;

        public void SetConfig(AbilityConfig config)
        {
            _abilityConfig = config;
        }
        public abstract void Use(AbilityParams useParams);

        protected void PlayParticleEffect()
        {
            var particlePrefab = _abilityConfig.ParticleEffect;
            var particles = Instantiate(particlePrefab, transform.position, particlePrefab.transform.rotation);
            particles.transform.parent = transform;
            var particlesComponenet = particles.GetComponent<ParticleSystem>();
            particlesComponenet.Play();
            Destroy(particles, particlesComponenet.main.duration);
        }

        protected void PlayAbilitySound()
        {
            var abilitySound = _abilityConfig.AbilitySound;
            var audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(abilitySound);
        }
    }
}
