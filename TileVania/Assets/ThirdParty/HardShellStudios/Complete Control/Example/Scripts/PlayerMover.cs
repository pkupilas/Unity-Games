using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {

    public float moveForce = 1;
    public float jumpForce = 100;

	void Update ()
    {
        if (hInput.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(hInput.GetAxis("Horizontal") * moveForce, GetComponent<Rigidbody2D>().velocity.y);
    }
}
