using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private float _speed = 25f;

    void Update ()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        var moveVector = new Vector3(h, v, 0f) * _speed * Time.deltaTime;

        transform.Translate(moveVector);
    }
}
