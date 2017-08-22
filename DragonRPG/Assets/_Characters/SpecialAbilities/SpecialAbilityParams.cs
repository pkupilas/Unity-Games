using _Characters.Enemies;

namespace _Characters.SpecialAbilities
{
    public class SpecialAbilityParams
    {
        public Enemy Target { get; set; }
        public float Damage { get; set; }

        public SpecialAbilityParams(Enemy target, float damage)
        {
            Target = target;
            Damage = damage;
        }
    }
}
