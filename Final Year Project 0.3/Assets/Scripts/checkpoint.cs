using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class checkpoint : MonoBehaviour
{
    public Vector2 lastPos;
    public bool isDead;
    public Image hitEffect;
    float hitTimer;
    float freezeTimer;


    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        hitEffect.enabled = false;
        hitTimer = 1f;
        freezeTimer = 1f;

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            freezeTimer -= Time.fixedDeltaTime;
            Time.timeScale = 0;


        }

        if(freezeTimer <= 0)
        {
            Time.timeScale = 1;
            transform.position = lastPos;
            isDead = false;
            freezeTimer = 1f;
            hitEffect.enabled = false;
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            lastPos = collision.gameObject.transform.position;

        }

        if (collision.CompareTag("Spike"))
        {
            isDead = true;
            hitEffect.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spike")
        {
            isDead = true;
            hitEffect.enabled = true;
        }
    }


}
