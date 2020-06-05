using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed; //speed of bullet
    public float bTimer; //destroy bullet after this time
    public float Brange; //range of wall hit collider and interact layer collider
    public float attackRange; //range of collider for attacking enemies
    public float enemyRange;
    public int BulletDamage;

    public LayerMask interactLayers; //specified layers for bullet to interact with
    public LayerMask WGLayer; //specified layers for bullets to stick to
    public LayerMask enemyLayer; //specified layers for bullet to hit enemies with
    public LayerMask BossLayer;
    public LayerMask Boss02Layer;
    public LayerMask BombLayer;

    bool moving; //whether bullet is moving or not

    private Rigidbody2D rb; //referance to bullets rigidbody 2D

    public GameObject hitEffect;

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        Speed = 20f; //setting bullet speed
        rb = GetComponent<Rigidbody2D>(); // setting rigidbody 2D referance
        bTimer = 2f; //setting timer value
        moving = true; //setting whether bullet is moving
        BulletDamage = 2;

    }

    // Update is called once per frame
    void Update()
    {
        bTimer -= Time.deltaTime; //decreasing timer value when spawned

        //resetting timer and destroying bullet when timer has decreased below 0
        if (bTimer <= 0)
        {
            bTimer = 2f;
            Destroy(gameObject);
        }

        //bullet colliders

        Collider2D bulletHit = Physics2D.OverlapCircle(transform.position, Brange, interactLayers); //setting the interaction collider (interaction with environment)

        if (bulletHit != null) //what happens when the bullet hits an object on the interaction layer
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
            bulletHit.GetComponent<SpikeScript>().dead = true;
            Destroy(gameObject, 0.01f);
        }

        Collider2D wallHit = Physics2D.OverlapCircle(transform.position, Brange, WGLayer); //setting the wall collider (bullet sticks to wall)

        if (wallHit != null) //what happens when bullet hits an object on the wall layer
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject, 0.01f);

        }

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, enemyRange, enemyLayer); //setting the enemy collider (bullet stuns an enemy)

        foreach (Collider2D enemy in enemiesHit) // what to do when a bullet collides with an enemy on the enemy layer
        {
            Speed = 0;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            enemy.GetComponent<EnemyScript>().EnemeyStun();
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        Collider2D BossHit = Physics2D.OverlapCircle(transform.position, attackRange, BossLayer);

        if (BossHit != null)
        {
            BossHit.GetComponent<Boss01>().TakeDamage(BulletDamage * 5);
            BossHit.GetComponent<Boss01>().animPaused = true;
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        Collider2D Boss02Hit = Physics2D.OverlapCircle(transform.position, attackRange, Boss02Layer);

        if (Boss02Hit != null)
        {
            Boss02Hit.GetComponentInParent<FinalBoss>().TakeDamage(BulletDamage * 2);
            Instantiate(hitEffect, transform.position, transform.rotation);
            if (Boss02Hit.GetComponentInParent<FinalBoss>().isIdle)
            {
                Boss02Hit.GetComponentInParent<FinalBoss>().Boss02Anim.SetBool("isHit", true);
            }
            Destroy(gameObject);

        }

        Collider2D bombHit = Physics2D.OverlapCircle(transform.position, attackRange, BombLayer);

        if (bombHit != null)
        {
            bombHit.GetComponent<HomingEnemy>().Die();
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);

        }

    }

    void FixedUpdate() //moves the bullet
    {
        if (moving)
        {
            rb.velocity = transform.right * Speed;

        }
    }

    private void OnDrawGizmos() //draws the colliders in the scene view
    {
        if (transform.position == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(transform.position, Brange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, enemyRange);
    }
}
