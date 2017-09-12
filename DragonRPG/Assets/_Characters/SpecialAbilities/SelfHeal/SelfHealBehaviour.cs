using UnityEngine;
using _Characters;
using _Characters.SpecialAbilities;

public class SelfHealBehaviour : MonoBehaviour, ISpecialAbility
{
    private SelfHealConfig _selfHealConfig;
    private Player _player;

    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    public void SetConfig(SelfHealConfig config)
    {
        _selfHealConfig = config;
    }
    
    public void Use(SpecialAbilityParams useParams)
    {
        _player.ChangeHealth(-_selfHealConfig.HealAmount);
        PlayParticleEffect();
    }

    private void PlayParticleEffect()
    {
        var particles = Instantiate(_selfHealConfig.GetParticleEffect(), transform.position, Quaternion.identity);
        var particlesComponenet = particles.GetComponent<ParticleSystem>();
        particlesComponenet.Play();
        Destroy(particles, particlesComponenet.main.duration);
    }
}
