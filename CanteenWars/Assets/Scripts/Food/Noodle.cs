using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noodle : Food
{
    public override float Acceleration { get; set; }
    public override float Damage { get; set; }

    private FoodSpawner _foodSpawner;

    protected override void Awake()
    {
        base.Awake();
        Acceleration = 950;
        Damage = 60;
    }

    void Start()
    {
        _foodSpawner = FindObjectOfType<FoodSpawner>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var otherFood = other.gameObject.GetComponent<Sauce>();
        if (otherFood != null && !_animator.GetBool("isDestroying"))
        {
            if (gameObject.transform.position.x > 0)
            {
                _foodSpawner.SpawnComboShot("Enemy");
            }
            else
            {
                _foodSpawner.SpawnComboShot("Player");
            }
        }

    }
}
