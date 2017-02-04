using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
    public GameObject projectile;
    public GameObject gun;

    private GameObject projectileParent;
    private Animator animator;
    private Spawner myLaneSpawner;

    void Start()
    {
        projectileParent = GameObject.Find("Projectiles") ?? CreateProjectileParent();
        animator = GetComponent<Animator>(); //FindObjectOfType<Animator>(); ?
        SetMyLaneSpawner();
    }
    private void Update()
    {
        animator.SetBool("isAttacking", IsAttackerAheadInLane());
    }

    private void SetMyLaneSpawner()
    {
        Spawner[] spawnerArray = FindObjectsOfType<Spawner>();
        var shooterPosition = transform.position;

        if (spawnerArray.Length < 0)
        {
            Debug.LogError(name + ": There is no spawners.");
            return;
        }

        foreach (Spawner spawner in spawnerArray)
        {
            if (spawner.transform.position.y == shooterPosition.y)
            {
                myLaneSpawner = spawner;
                break;
            }
        }
    }

    private bool IsAttackerAheadInLane()
    {
        if (!myLaneSpawner || myLaneSpawner.transform.childCount <=0) return false;

        foreach (Transform attacker in myLaneSpawner.transform)
        {
            if (attacker.position.x > transform.position.x)
            {
                return true;
            }
        }
        
        return false;
    }

    private GameObject CreateProjectileParent()
    {
        return new GameObject("Projectiles");
    }

    private void Fire()
    {
        if (!projectile || !projectileParent || !gun) return;
        var newProjectile = Instantiate(projectile) as GameObject;

        if (!newProjectile) return;
        newProjectile.transform.parent = projectileParent.transform;
        newProjectile.transform.position = gun.transform.position;
    }
}
