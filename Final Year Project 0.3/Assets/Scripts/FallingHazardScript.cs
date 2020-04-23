using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingHazardScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float Speed;
    public float Timer;

    private void Update()
    {
        Timer -= Time.deltaTime;

        if(Timer <= 0)
        {

            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * Speed;

    }
}
