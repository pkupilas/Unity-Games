using UnityEngine;
using _Core;

namespace _Characters.SpecialAbilities.AreaOfEffectAttack
{
    public class AreaOfEffectAttackBehaviour : MonoBehaviour, ISpecialAbility
    {
        private AreaOfEffectAttackConfig _areaOfEffectAttackConfig;

        public void SetConfig(AreaOfEffectAttackConfig config)
        {
            _areaOfEffectAttackConfig = config;
        }

        public void Use(SpecialAbilityParams useParams)
        {
            DealRadialDamage(useParams);
            PlayParticleEffect();
        }

        private void DealRadialDamage(SpecialAbilityParams useParams)
        {
            float finalDamage = useParams.Damage + _areaOfEffectAttackConfig.Damage;
            var radius = _areaOfEffectAttackConfig.Radius;
            var hitInfos = Physics.SphereCastAll(transform.position, radius, transform.forward, radius);

            foreach (var raycastHit in hitInfos)
            {
                if (!raycastHit.collider) continue;

                var target = raycastHit.collider.gameObject.GetComponent<IDamageable>();
                if (target != null)
                {
                    target.TakeDamage(finalDamage);
                }
            }
        }

        private void PlayParticleEffect()
        {
            var particles = Instantiate(_areaOfEffectAttackConfig.GetParticleEffect(), transform.position, Quaternion.identity);
            var particlesComponenet = particles.GetComponent<ParticleSystem>();
            particlesComponenet.Play();
            Destroy(particles,particlesComponenet.main.duration);
        }
    }
}
