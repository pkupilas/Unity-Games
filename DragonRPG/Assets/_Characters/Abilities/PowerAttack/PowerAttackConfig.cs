using UnityEngine;

namespace _Characters.Abilities.PowerAttack
{
    [CreateAssetMenu(menuName = "RPG/SpecialAbility/PowerAttack")]
    public class PowerAttackConfig : AbilityConfig
    {
        [Header("Power Attack Specific")]
        [SerializeField] private float _extraDamage;

        protected override void SetBehaviourComponent(GameObject target)
        {
            _behaviour = target.AddComponent<PowerAttackBehaviour>();
        }

        public float GetExtraDamage()
        {
            return _extraDamage;
        }
    }
}
