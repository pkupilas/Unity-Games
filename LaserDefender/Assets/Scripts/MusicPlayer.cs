using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _startClip;
    [SerializeField] private AudioClip _gameClip;
    [SerializeField] private AudioClip _endClip;

    private static MusicPlayer _instance;
    private AudioSource _audioSource;
    
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = _startClip;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }

    // TODO: Change due to OnLevelWasLoaded deprecated
    void OnLevelWasLoaded(int level)
    {
        _audioSource.Stop();
        if (level == 0)
        {
            _audioSource.clip = _startClip;
        }
        else if (level == 1)
        {
            _audioSource.clip = _gameClip;
        }
        else if (level == 2)
        {
            _audioSource.clip = _endClip;
        }
        _audioSource.loop = true;
        _audioSource.Play();
    }
}
