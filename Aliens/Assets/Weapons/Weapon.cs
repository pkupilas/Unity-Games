using Characters.Player;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;

    public WeaponData WeaponData => weaponData;

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
        if (Input.GetButtonDown("Fire1") && timer >= weaponData.AttackCooldown)
        {
            Shoot();
        }
    }

    protected abstract void Shoot();
}
