﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public float bTimer;
    public float Brange;
    public LayerMask interactLayers;
    public LayerMask stickLayer;
    bool moving;

    private Rigidbody2D rb;

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        Speed = 20f;
        rb = GetComponent<Rigidbody2D>();
        bTimer = 2f;
        moving = true;

    }

    // Update is called once per frame
    void Update()
    {          
        bTimer -= Time.deltaTime;

        if (bTimer <= 0)
        {
            bTimer = 2f;
            Destroy(gameObject);
        }

        Collider2D bulletHit = Physics2D.OverlapCircle(transform.position, Brange, interactLayers);

        if(bulletHit != null)
        {
            Destroy(gameObject);

        }

        Collider2D wallHit = Physics2D.OverlapCircle(transform.position, Brange, stickLayer);

        if(wallHit != null)
        {
            moving = false;

        }


    }

    void FixedUpdate()
    {
        if (moving)
        {
            rb.velocity = transform.right * Speed;

        }
    }

    private void OnDrawGizmos()
    {
        if(transform.position == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(transform.position, Brange);
    }
}
