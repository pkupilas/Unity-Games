using UnityEngine;
namespace _Characters.Abilities
{
    public abstract class AbilityBehaviour : MonoBehaviour
    {
        protected AbilityConfig AbilityConfig;

        public abstract void Use(AbilityParams useParams);
        protected abstract void PlayParticleEffect();
    }
}
