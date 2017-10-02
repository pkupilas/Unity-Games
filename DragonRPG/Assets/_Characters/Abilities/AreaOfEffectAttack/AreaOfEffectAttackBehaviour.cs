using UnityEngine;
using _Characters.CommonScripts;

namespace _Characters.Abilities.AreaOfEffectAttack
{
    public class AreaOfEffectAttackBehaviour : AbilityBehaviour
    {
        public override void Use(GameObject target)
        {
            DealRadialDamage();
            PlayParticleEffect();
            PlayAbilitySound();
        }

        private void DealRadialDamage()
        {
            var areaOfEffectAttackConfig = _abilityConfig as AreaOfEffectAttackConfig;
            float finalDamage = areaOfEffectAttackConfig.Damage;
            var radius = areaOfEffectAttackConfig.Radius;
            var hitInfos = Physics.SphereCastAll(transform.position, radius, transform.forward, radius);

            foreach (var raycastHit in hitInfos)
            {
                if (!raycastHit.collider) continue;

                var target = raycastHit.collider.gameObject.GetComponent<Health>();
                bool isPlayerHit = raycastHit.collider.gameObject.GetComponent<Player.PlayerMovement>();

                if (target != null && !isPlayerHit)
                {
                    target.TakeDamage(finalDamage);
                }
            }
        }
    }
}
