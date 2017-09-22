using _Characters.CommonScripts;

namespace _Characters.Abilities.PowerAttack
{
    public class PowerAttackBehaviour : AbilityBehaviour
    {
        public override void Use(AbilityParams useParams)
        {
            DealDamage(useParams);
            PlayParticleEffect();
            PlayAbilitySound();
        }

        private void DealDamage(AbilityParams useParams)
        {
            float finalDamage = useParams.PlayerBaseDamage + (_abilityConfig as PowerAttackConfig).GetExtraDamage();
            useParams.Target.GetComponent<Health>().TakeDamage(finalDamage);
        }
    }
}
