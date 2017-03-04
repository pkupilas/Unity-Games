using UnityEngine;
using System.Collections;

public class MyKeyFrame
{

    public float Time { get; private set; }
    public Vector3 Pos { get; private set; }
    public Quaternion Rot { get; private set; }

    public MyKeyFrame(float time, Vector3 pos, Quaternion rot)
    {
        Time = time;
        Pos = pos;
        Rot = rot;
    }
}
