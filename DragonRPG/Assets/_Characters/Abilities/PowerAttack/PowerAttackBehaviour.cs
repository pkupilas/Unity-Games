using UnityEngine;
using _Characters.CommonScripts;

namespace _Characters.Abilities.PowerAttack
{
    public class PowerAttackBehaviour : AbilityBehaviour
    {
        public override void Use(GameObject target)
        {
            DealDamage(target);
            PlayParticleEffect();
            PlayAbilitySound();
        }

        private void DealDamage(GameObject target)
        {
            var powerAttackConfig = _abilityConfig as PowerAttackConfig;
            float finalDamage = powerAttackConfig.GetExtraDamage();
            target.GetComponent<Health>().TakeDamage(finalDamage);
        }
    }
}
