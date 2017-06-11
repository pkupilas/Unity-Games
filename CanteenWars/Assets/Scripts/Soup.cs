using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soup : Food
{

    public override float Acceleration { get; set; }
    public override float Damage { get; set; }

    protected override void Awake()
    {
        base.Awake();
        Acceleration = 1000;
        Damage = 100;
    }

}
