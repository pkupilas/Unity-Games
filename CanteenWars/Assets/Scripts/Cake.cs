using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : Food
{

    protected override float Acceleration { get; set; }

    protected override void Awake()
    {
        base.Awake();
        Acceleration = 500;
    }

}
