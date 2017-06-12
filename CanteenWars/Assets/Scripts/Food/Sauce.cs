using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sauce : Food
{

    public override float Acceleration { get; set; }
    public override float Damage { get; set; }


    protected override void Awake()
    {
        base.Awake();
        Acceleration = 850;
        Damage = 70;
    }
}
