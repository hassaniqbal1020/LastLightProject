using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public float bTimer;

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

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * Speed;
            
        bTimer -= Time.deltaTime;

        if (bTimer <= 0)
        {
            bTimer = 2f;
            Destroy(gameObject);


        }

    }
}
