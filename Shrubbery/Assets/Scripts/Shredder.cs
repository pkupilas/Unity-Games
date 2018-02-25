using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var component = other.gameObject.GetComponent<Projectile>();
        if (component)
        {
            Destroy(other.gameObject);
        }
    }
}
