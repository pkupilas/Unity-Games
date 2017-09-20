using UnityEngine;

namespace _Characters.Abilities.AreaOfEffectAttack
{
    [CreateAssetMenu(menuName = "RPG/SpecialAbility/AreaOfEffectAttack")]
    public class AreaOfEffectAttackConfig : AbilityConfig
    {
        [Header("Area Of Effect Attack Specific")]
        [SerializeField] private float _damage;
        [SerializeField] private float _radius;

        protected override void SetBehaviourComponent(GameObject target)
        {
            behaviour = target.AddComponent<AreaOfEffectAttackBehaviour>();
        }

        public float Radius { get { return _radius;} }
        public float Damage { get { return _damage; } }
    }
}
