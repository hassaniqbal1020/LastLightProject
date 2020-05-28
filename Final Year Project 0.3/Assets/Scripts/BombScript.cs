using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public LayerMask PlayerLayer;
    public float BlastRange;
    public float BlastDamage;

    public GameObject hitEffect;

    public float BombTimer;

    // Start is called before the first frame update
    void Start()
    {
        BombTimer = 0.5f;
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

        if(BombTimer <= 0)
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            BombTimer -= Time.deltaTime;
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
