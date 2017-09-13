using UnityEngine;
using _Core;

namespace _Characters.SpecialAbilities.AreaOfEffectAttack
{
    public class AreaOfEffectAttackBehaviour : MonoBehaviour, IAbility
    {
        private AreaOfEffectAttackConfig _areaOfEffectAttackConfig;
        private AudioSource _audioSource;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void SetConfig(AreaOfEffectAttackConfig config)
        {
            _areaOfEffectAttackConfig = config;
        }

        public void Use(AbilityParams useParams)
        {
            DealRadialDamage(useParams);
            _audioSource.clip = _areaOfEffectAttackConfig.AbilitySound;
            _audioSource.Play();
            PlayParticleEffect();
        }

        private void DealRadialDamage(AbilityParams useParams)
        {
            float finalDamage = useParams.PlayerBaseDamage + _areaOfEffectAttackConfig.Damage;
            var radius = _areaOfEffectAttackConfig.Radius;
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

        private void PlayParticleEffect()
        {
            var particlePrefab = _areaOfEffectAttackConfig.ParticleEffect;
            var particles = Instantiate(particlePrefab, transform.position, particlePrefab.transform.rotation);
            var particlesComponenet = particles.GetComponent<ParticleSystem>();
            particlesComponenet.Play();
            Destroy(particles,particlesComponenet.main.duration);
        }
    }
}
