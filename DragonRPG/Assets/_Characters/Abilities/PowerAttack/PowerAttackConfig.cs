using UnityEngine;

namespace _Characters.SpecialAbilities.PowerAttack
{
    [CreateAssetMenu(menuName = "RPG/SpecialAbility/PowerAttack")]
    public class PowerAttackConfig : AbilityConfig
    {
        [Header("Power Attack Specific")]
        [SerializeField] private float _extraDamage;

        public override void AttachComponentTo(GameObject gameObject)
        {
            var behaviourComponent = gameObject.AddComponent<PowerAttackBehaviour>();
            behaviourComponent.SetConfig(this);
            behaviour = behaviourComponent;
        }

        public float GetExtraDamage()
        {
            return _extraDamage;
        }
    }
}
