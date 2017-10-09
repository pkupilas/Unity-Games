using UnityEngine;
using _Characters.Player;

namespace _Characters.Audio
{
    public class AudioTrigger : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private float _maxTriggerDistance = 2f;
        [SerializeField] private bool _isOneTimeOnly = true;

        private bool _hasPlayed;
        private AudioSource _audioSource;
        private PlayerControl _player;

        void Start()
        {
            _player = FindObjectOfType<PlayerControl>();
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.volume = 0.25f;
            _audioSource.playOnAwake = false;
            _audioSource.clip = _clip;
        }

        void Update()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, _player.gameObject.transform.position);
            if (distanceToPlayer <=_maxTriggerDistance)
            {
                RequestPlayAudioClip();
            }
        }

        private void RequestPlayAudioClip()
        {
            if (_isOneTimeOnly && _hasPlayed)
            {
                return;
            }
            else if (_audioSource.isPlaying == false)
            {
                _audioSource.Play();
                _hasPlayed = true;
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 255f, 0, .5f);
            Gizmos.DrawWireSphere(transform.position, _maxTriggerDistance);
        }
    }
}