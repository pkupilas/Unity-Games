using UnityEngine;

namespace _Characters.SpecialAbilities
{
    public abstract class SpecialAbilityConfig : ScriptableObject
    {
        [Header("Special Ability General")]
        [SerializeField] private float _energyCost;

        protected ISpecialAbility behaviour;

        public abstract void AttachComponentTo(GameObject gameObject);

        public void UseAbility(SpecialAbilityParams useParams)
        {
            behaviour.Use(useParams);
        }

        public float GetEnergyCost()
        {
            return _energyCost;
        }
    }
}