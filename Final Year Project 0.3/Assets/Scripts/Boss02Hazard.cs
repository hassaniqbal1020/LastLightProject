using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Hazard : MonoBehaviour
{
    public Rigidbody2D rb;
    public float Speed;
    public float Timer;

    public float hitRange;
    public LayerMask playerLayer;

    public GameObject hitEffect;

    private void Update()
    {
        Collider2D hitBox = Physics2D.OverlapCircle(transform.position, hitRange, playerLayer);

        if(hitBox != null)
        {
            hitBox.GetComponent<PlayerMovement>().LifeRadial.fillAmount -= 0.1f;
            hitBox.GetComponent<PlayerMovement>().hitByEnemy = true;
            hitBox.GetComponent<PlayerMovement>().timeFreeze = true;
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        Timer -= Time.deltaTime;

        if (Timer <= 0)
        {

            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * Speed;

    }

    private void OnDrawGizmos()
    {
        if(transform.position == null)
        {
            return;

        }
        Gizmos.DrawWireSphere(transform.position, hitRange);
    }
}
