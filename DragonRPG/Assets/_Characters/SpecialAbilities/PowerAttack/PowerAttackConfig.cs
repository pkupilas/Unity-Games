using UnityEngine;

namespace _Characters.SpecialAbilities.PowerAttack
{
    [CreateAssetMenu(menuName = "RPG/SpecialAbility/PowerAttack")]
    public class PowerAttackConfig : SpecialAbilityConfig
    {
        [Header("Power Attack Specific")]
        [SerializeField] private float _extraDamage;

        public override ISpecialAbility AddComponent(GameObject gameObject)
        {
            var behaviour = gameObject.AddComponent<PowerAttackBehaviour>();
            behaviour.SetConfig(this);

            return behaviour;
        }
    }
}
