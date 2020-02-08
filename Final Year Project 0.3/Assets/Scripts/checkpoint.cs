using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public Vector2 lastPos;
    public bool isDead;
    float dTimer;

    // Start is called before the first frame update
    void Start()
    {
        dTimer = 1.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            dTimer -= Time.deltaTime;

        }

        if(dTimer <= 0)
        {
            gameObject.transform.position = lastPos;
            isDead = false;
            dTimer = 1.5f;


        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            lastPos = collision.gameObject.transform.position;

        }
    }
}
