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
            float finalDamage = useParams.Damage + _powerAttackConfig.GetExtraDamage();
            useParams.Target.TakeDamage(finalDamage);
        }
    }
}
