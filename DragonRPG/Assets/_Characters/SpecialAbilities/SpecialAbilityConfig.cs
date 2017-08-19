using UnityEngine;

namespace _Characters.SpecialAbilities
{
    public abstract class SpecialAbilityConfig : ScriptableObject
    {
        [Header("Special Ability General")]
        [SerializeField] private float _energyCost;

        protected ISpecialAbility behaviour;

        public abstract void AttachComponentTo(GameObject gameObject);

        public void Use()
        {
            behaviour.Use();
        }
    }
}