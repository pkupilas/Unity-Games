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
        
        public override void Use(AbilityParams useParams)
        {
            DealDamage(useParams);
            _audioSource.clip = _abilityConfig.AbilitySound;
            _audioSource.Play();
            PlayParticleEffect();
        }

        private void DealDamage(AbilityParams useParams)
        {
            float finalDamage = useParams.PlayerBaseDamage + (_abilityConfig as PowerAttackConfig).GetExtraDamage();
            useParams.Target.TakeDamage(finalDamage);
        }
    }
}
