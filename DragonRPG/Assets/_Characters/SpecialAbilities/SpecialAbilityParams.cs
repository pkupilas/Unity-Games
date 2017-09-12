using _Core;

namespace _Characters.SpecialAbilities
{
    public class SpecialAbilityParams
    {
        public IDamageable Target { get; set; }
        public float PlayerBaseDamage { get; set; }

        public SpecialAbilityParams(IDamageable target, float playerBaseDamage)
        {
            Target = target;
            PlayerBaseDamage = playerBaseDamage;
        }
    }
}
