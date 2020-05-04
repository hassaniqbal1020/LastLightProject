using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed; //speed of bullet
    public float bTimer; //destroy bullet after this time
    public float Brange; //range of wall hit collider and interact layer collider
    public float attackRange; //range of collider for attacking enemies

    public LayerMask interactLayers; //specified layers for bullet to interact with
    public LayerMask stickLayer; //specified layers for bullets to stick to
    public LayerMask enemyLayer; //specified layers for bullet to hit enemies with

    bool moving; //whether bullet is moving or not

    private Rigidbody2D rb; //referance to bullets rigidbody 2D

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

        if(bulletHit != null) //what happens when the bullet hits an object on the interaction layer
        {
            Destroy(gameObject);
            bulletHit.GetComponent<SpikeScript>().dead = true;

        }

        Collider2D wallHit = Physics2D.OverlapCircle(transform.position, Brange, stickLayer); //setting the wall collider (bullet sticks to wall)

        if(wallHit != null) //what happens when bullet hits an object on the wall layer
        {
            moving = false;

        }

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer); //setting the enemy collider (bullet stuns an enemy)

        foreach(Collider2D enemy in enemiesHit) // what to do when a bullet collides with an enemy on the enemy layer
        {
            enemy.GetComponent<EnemyScript>().EnemeyStun();
            rb.velocity = new Vector2(0, 0);
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
        if(transform.position == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(transform.position, Brange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
