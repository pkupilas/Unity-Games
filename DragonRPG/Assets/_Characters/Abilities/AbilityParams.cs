using _Characters.Enemies;
using _Core;

namespace _Characters.Abilities
{
    public class AbilityParams
    {
        public Enemy Target { get; set; }
        public float PlayerBaseDamage { get; set; }

        public AbilityParams(Enemy target, float playerBaseDamage)
        {
            Target = target;
            PlayerBaseDamage = playerBaseDamage;
        }
    }
}
