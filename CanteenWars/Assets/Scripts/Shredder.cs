using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        var food = other.gameObject.GetComponent<Food>();
        if (food)
        {
            Debug.Log("Collision with food.");
            var foodAnimator = food.GetComponent<Animator>();
            foodAnimator.SetTrigger("isDestroying");
        }
    }
}
