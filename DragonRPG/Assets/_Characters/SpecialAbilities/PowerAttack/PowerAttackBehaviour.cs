using UnityEngine;

namespace _Characters.SpecialAbilities.PowerAttack
{
    public class PowerAttackBehaviour : MonoBehaviour, ISpecialAbility
    {
        private PowerAttackConfig _powerAttackConfig;

        public void SetConfig(PowerAttackConfig config)
        {
            _powerAttackConfig = config;
        }

        public void Use(SpecialAbilityParams useParams)
        {
            DealDamage(useParams);
            PlayParticleEffect();
        }

        private void DealDamage(SpecialAbilityParams useParams)
        {
            float finalDamage = useParams.PlayerBaseDamage + _powerAttackConfig.GetExtraDamage();
            useParams.Target.ChangeHealth(finalDamage);
        }

        private void PlayParticleEffect()
        {
            var particles = Instantiate(_powerAttackConfig.GetParticleEffect(), transform.position, Quaternion.identity);
            var particlesComponenet = particles.GetComponent<ParticleSystem>();
            particlesComponenet.Play();
            Destroy(particles, particlesComponenet.main.duration);
        }
    }
}
