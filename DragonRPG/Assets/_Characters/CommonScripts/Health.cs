using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Characters.CommonScripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _deathSounds;
        [SerializeField] private List<AudioClip> _takeDamageSounds;
        [SerializeField] private float _deathVanishTime = 2f;
        [SerializeField] private float _maxHealth = 100f;

        private Animator _animator;
        private AudioSource _audioSource;

        private const string DeathTrigger = "DeathTrigger";

        public float HealthAsPercentage => CurrentHealth / _maxHealth;
        public float CurrentHealth { get; set; }
        public bool IsAlive => CurrentHealth > Mathf.Epsilon;

        void Start ()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            SetCurrentHealthToMax();
        }

        private void SetCurrentHealthToMax()
        {
            CurrentHealth = _maxHealth;
        }

        private void PlaySound(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }

        private IEnumerator KillCharacter()
        {
            _animator.SetTrigger(DeathTrigger);

            var deathClip = GetRandomClipFrom(_deathSounds);
            PlaySound(deathClip);
            yield return new WaitForSeconds(deathClip.length);

            var playerComponent = GetComponent<Player.PlayerControl>();
            if (playerComponent && playerComponent.isActiveAndEnabled)
            {

                SceneManager.LoadScene(0);
            }
            else
            {
                DestroyObject(gameObject, _deathVanishTime);
            }
        }

        private AudioClip GetRandomClipFrom(List<AudioClip> sounds)
        {
            int randomIndex = Random.Range(0, sounds.Count);
            return sounds[randomIndex];
        }

        public void Heal(float healthPoints)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + healthPoints, 0f, _maxHealth);
        }

        public void TakeDamage(float damage)
        {
            PlaySound(GetRandomClipFrom(_takeDamageSounds));
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0f, _maxHealth);
            
            if (!IsAlive)
            {
                StartCoroutine(KillCharacter());
            }
        }
    }
}
