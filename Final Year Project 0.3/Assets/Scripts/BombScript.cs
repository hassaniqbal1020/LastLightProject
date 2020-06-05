using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public LayerMask PlayerLayer;
    public float BlastRange;
    public float BlastDamage;

    public GameObject hitEffect;

    public float BombTimer;

    public bool Activate;

    private void Awake()
    {
        BombTimer = 0.5f;

    }
    // Start is called before the first frame update
    void Start()
    {
        Activate = false;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D Blast = Physics2D.OverlapCircle(transform.position, BlastRange, PlayerLayer);

        if(Blast != null)
        {
            Blast.GetComponent<PlayerMovement>().LifeRadial.fillAmount -= 0.1f;
            Blast.GetComponent<PlayerMovement>().hitByEnemy = true;
            Blast.GetComponent<PlayerMovement>().timeFreeze = true;
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);

        }

        if (Activate)
        {
            BombTimer -= Time.deltaTime;

        }

        if (BombTimer <= 0)
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Activate = true;

        }
    }

    private void OnDrawGizmos()
    {
        if(transform.position == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(transform.position, BlastRange);
    }
}
