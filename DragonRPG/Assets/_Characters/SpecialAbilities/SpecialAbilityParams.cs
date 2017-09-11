using _Characters.Enemies;
using _Core;

namespace _Characters.SpecialAbilities
{
    public class SpecialAbilityParams
    {
        public IDamageable Target { get; set; }
        public float Damage { get; set; }

        public SpecialAbilityParams(IDamageable target, float damage)
        {
            Target = target;
            Damage = damage;
        }
    }
}
