using UnityEngine;
using _Characters.CommonScripts;

namespace _Characters.Abilities
{
    public abstract class AbilityBehaviour : MonoBehaviour
    {
        protected AbilityConfig _abilityConfig;
        private const string AttackTrigger = "AttackTrigger";
        private const string AttackAnimationName = "DEAFAULT ATTACK";

        public void SetConfig(AbilityConfig config)
        {
            _abilityConfig = config;
        }

        public abstract void Use(GameObject target);

        protected void PlayParticleEffect()
        {
            var particlePrefab = _abilityConfig.ParticleEffect;
            var particles = Instantiate(particlePrefab, transform.position, particlePrefab.transform.rotation);
            particles.transform.parent = transform;
            var particlesComponenet = particles.GetComponent<ParticleSystem>();
            particlesComponenet.Play();
            Destroy(particles, particlesComponenet.main.duration);
        }

        protected void PlayAbilitySound()
        {
            var abilitySound = _abilityConfig.AbilitySound;
            var audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(abilitySound);
        }

        protected void PlayAbilityAnimation()
        {
            var animator = GetComponent<Animator>();
            var animatorOverrideController = GetComponent<Character>().AnimatorOverrideController;
            animator.runtimeAnimatorController = animatorOverrideController;
            animatorOverrideController[AttackAnimationName] = _abilityConfig.GetAbilityAnimationClip();
            animator.SetTrigger(AttackTrigger);
        }
    }
}
