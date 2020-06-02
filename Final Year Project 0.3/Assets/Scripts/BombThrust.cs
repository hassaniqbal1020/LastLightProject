using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrust : MonoBehaviour
{
    public float ThrustForce;

    private Rigidbody2D rb;

    private void Awake()
    {
        ThrustForce = 13f;
        rb = gameObject.GetComponent<Rigidbody2D>();

        rb.AddForce(transform.up * ThrustForce, ForceMode2D.Impulse);

    }
}
