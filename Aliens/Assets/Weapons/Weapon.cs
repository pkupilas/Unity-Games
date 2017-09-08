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

    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();
        timer = weaponData.AttackCooldown;
        autoTarget = GetComponent<AutoTarget>();
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer >= weaponData.AttackCooldown)
        {
            //player.GetComponent<Animator>().SetTrigger(AttackTrigger);
            Shoot();
        }
    }

    protected abstract void Shoot();
}
