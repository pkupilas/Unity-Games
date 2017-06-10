using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        var food = other.gameObject.GetComponent<Food>();
        if (food)
        {
            Destroy(other.gameObject);
        }
    }
}
