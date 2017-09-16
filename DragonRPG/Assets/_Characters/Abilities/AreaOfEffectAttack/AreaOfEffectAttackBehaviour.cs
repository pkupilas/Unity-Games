using UnityEngine;
using _Core;

namespace _Characters.Abilities.AreaOfEffectAttack
{
    public class AreaOfEffectAttackBehaviour : AbilityBehaviour
    {
        private AudioSource _audioSource;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void SetConfig(AbilityConfig config)
        {
            AbilityConfig = config;
        }

        public override void Use(AbilityParams useParams)
        {
            DealRadialDamage(useParams);
            _audioSource.clip = AbilityConfig.AbilitySound;
            _audioSource.Play();
            PlayParticleEffect();
        }

        private void DealRadialDamage(AbilityParams useParams)
        {

            var areaOfEffectAttackConfig = AbilityConfig as AreaOfEffectAttackConfig;
            float finalDamage = useParams.PlayerBaseDamage + areaOfEffectAttackConfig.Damage;
            var radius = areaOfEffectAttackConfig.Radius;
            var hitInfos = Physics.SphereCastAll(transform.position, radius, transform.forward, radius);

            foreach (var raycastHit in hitInfos)
            {
                if (!raycastHit.collider) continue;

                var target = raycastHit.collider.gameObject.GetComponent<IDamageable>();
                bool isPlayerHit = raycastHit.collider.gameObject.GetComponent<Player>();

                if (target != null && !isPlayerHit)
                {
                    target.TakeDamage(finalDamage);
                }
            }
        }

        protected override void PlayParticleEffect()
        {
            var particlePrefab = AbilityConfig.ParticleEffect;
            var particles = Instantiate(particlePrefab, transform.position, particlePrefab.transform.rotation);
            var particlesComponenet = particles.GetComponent<ParticleSystem>();
            particlesComponenet.Play();
            Destroy(particles,particlesComponenet.main.duration);
        }
    }
}
