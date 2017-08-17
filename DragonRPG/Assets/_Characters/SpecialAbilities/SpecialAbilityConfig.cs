using UnityEngine;

namespace _Characters.SpecialAbilities
{
    public abstract class SpecialAbilityConfig : ScriptableObject
    {
        [Header("Special Ability General")]
        [SerializeField] private float _energyCost;

        public abstract ISpecialAbility AddComponent(GameObject gameObject);
    }
}