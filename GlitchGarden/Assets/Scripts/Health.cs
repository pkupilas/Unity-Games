using UnityEngine;
using System.Collections;
using System;

public class Health : MonoBehaviour
{
    public float health = 100f;

    public void DealDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
