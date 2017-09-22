using _Characters.CommonScripts;

namespace _Characters.Abilities.SelfHeal
{
    public class SelfHealBehaviour : AbilityBehaviour
    {
        public override void Use(AbilityParams useParams)
        {
            ApplyHeal();
            PlayParticleEffect();
            PlayAbilitySound();
        }

        private void ApplyHeal()
        {
            var selfHealConfig = _abilityConfig as SelfHealConfig;
            var health = GetComponent<Health>();
            health.Heal(selfHealConfig.HealAmount);
        }
    }
}
