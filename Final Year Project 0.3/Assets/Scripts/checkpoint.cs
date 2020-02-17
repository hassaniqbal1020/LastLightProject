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

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        hitEffect.enabled = false;
        hitTimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            transform.position = lastPos;
            isDead = false;

        }

        if (hitEffect.enabled == true)
        {
            hitTimer -= Time.deltaTime;
        }

        if(hitTimer <= 0)
        {
            hitEffect.enabled = false;
            hitTimer = 1f;

        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            lastPos = collision.gameObject.transform.position;

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
