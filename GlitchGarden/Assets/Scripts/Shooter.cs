using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
    public GameObject projectile;
    public GameObject projectileParent;
    public GameObject gun;

    private void Fire()
    {
        if (!projectile || !projectileParent || !gun) return;
        GameObject newProjectile = Instantiate(projectile) as GameObject;

        if (!newProjectile) return;
        newProjectile.transform.parent = projectileParent.transform;
        newProjectile.transform.position = gun.transform.position;
    }
}
