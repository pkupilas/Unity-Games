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

        public void Use()
        {

        }
    }
}
