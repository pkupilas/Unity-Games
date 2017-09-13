using UnityEngine;
using _Characters;
using _Characters.SpecialAbilities;

public class SelfHealBehaviour : MonoBehaviour, IAbility
{
    private SelfHealConfig _selfHealConfig;
    private Player _player;
    private AudioSource _audioSource;

    void Start()
    {
        _player = GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetConfig(SelfHealConfig config)
    {
        _selfHealConfig = config;
    }
    
    public void Use(AbilityParams useParams)
    {
        _player.Heal(_selfHealConfig.HealAmount);
        _audioSource.clip = _selfHealConfig.AbilitySound;
        _audioSource.Play();
        PlayParticleEffect();
    }

    private void PlayParticleEffect()
    {
        var particles = Instantiate(_selfHealConfig.ParticleEffect, transform.position, Quaternion.identity);
        particles.transform.parent = _player.transform;
        var particlesComponenet = particles.GetComponent<ParticleSystem>();
        particlesComponenet.Play();
        Destroy(particles, particlesComponenet.main.duration);
    }
}
