using UnityEngine;

namespace _Characters.SpecialAbilities.AreaOfEffectAttack
{
    [CreateAssetMenu(menuName = "RPG/SpecialAbility/AreaOfEffectAttack")]
    public class AreaOfEffectAttackConfig : SpecialAbilityConfig
    {
        [Header("Area Of Effect Attack Specific")]
        [SerializeField] private float _damage;
        [SerializeField] private float _radius;

        public override void AttachComponentTo(GameObject gameObject)
        {
            var behaviourComponent = gameObject.AddComponent<AreaOfEffectAttackBehaviour>();
            behaviourComponent.SetConfig(this);
            behaviour = behaviourComponent;
        }

        public float Radius { get { return _radius;} }
        public float Damage { get { return _damage; } }
    }
}
