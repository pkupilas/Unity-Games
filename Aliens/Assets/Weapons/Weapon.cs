using Characters.Player;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;
    
    public WeaponData WeaponData => weaponData;

    private const string AttackTrigger = "AttackTrigger";

    protected Player player;
    protected float timer;
    protected AutoTarget autoTarget;
    private AudioSource _audioSource;

    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();
        timer = weaponData.AttackCooldown;
        autoTarget = GetComponent<AutoTarget>();
        _audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer >= weaponData.AttackCooldown)
        {
            Shoot();
        }
    }

    protected void PlayWeaponSound()
    {
        _audioSource.clip = weaponData.AudioClip;
        _audioSource.Play();
    }

    protected abstract void Shoot();
}
