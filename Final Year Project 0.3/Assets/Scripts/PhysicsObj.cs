﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObj : MonoBehaviour
{
    public float GravityMod = 1f;

    protected Rigidbody2D rb;
    protected Vector2 velocity;
    protected const float minDist = 0.001f;


    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        velocity += GravityMod * Physics2D.gravity * Time.deltaTime;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 move = Vector2.up * deltaPosition.y;

        Movement (move);

    }

    void Movement (Vector2 move)
    {
        float distance = move.magnitude;

        if(distance > minDist)
        {
            
        }
        rb.position = rb.position + move;

    }
}
