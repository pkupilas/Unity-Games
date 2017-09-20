using UnityEngine;

namespace _Characters.Abilities.SelfHeal
{
    [CreateAssetMenu(menuName = "RPG/SpecialAbility/SelfHeal")]
    public class SelfHealConfig : AbilityConfig
    {
        [Header("Self Heal Specific")]
        [SerializeField] private float _healAmount;

        public float HealAmount => _healAmount;

        protected override void SetBehaviourComponent(GameObject target)
        {
            behaviour = target.AddComponent<SelfHealBehaviour>();
        }
    }
}
