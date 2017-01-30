using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
    public GameObject projectile;
    public GameObject gun;

    private GameObject projectileParent;

    void Start()
    {
        projectileParent = GameObject.Find("Projectiles") ?? CreateProjectileParent();
    }

    private GameObject CreateProjectileParent()
    {
        return new GameObject("Projectiles");
    }


    private void Fire()
    {
        if (!projectile || !projectileParent || !gun) return;
        GameObject newProjectile = Instantiate(projectile) as GameObject;

        if (!newProjectile) return;
        newProjectile.transform.parent = projectileParent.transform;
        newProjectile.transform.position = gun.transform.position;
    }
}
