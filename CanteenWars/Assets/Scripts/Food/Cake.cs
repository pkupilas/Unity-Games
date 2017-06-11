using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : Food
{

    public override float Acceleration { get; set; }
    public override float Damage { get; set; }

    protected override void Awake()
    {
        base.Awake();
        Acceleration = 1400;
        Damage = 50;
    }

}
