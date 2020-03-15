using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float maxDistance; // Max distance of the enemy raycast
    public float attackTime; // Time until next attack
    public float activeStateTimer; // Timer for when enemy becomes active before attacking
    public float speed; // Walking speed of the player
    public float AttackSpeed; // Speed of enemy when in attacking state
    public float stopDistance; // Stopping distance between enemy and player
    public float attackRange; // Range of enemy attack
    public float AttackStateTimer; // Timer for enemy attack state
    public float WalkRange; // Radius of floor collider

    [SerializeField] private bool EnemyFacingRight; // Direction the enemy is facing
    public bool canAttack; // Whether enemy can attack or not
    bool FacingRight; // Direction the enemy is facing
    bool isFloor; // Whether floor is in front of the enemy
    bool Playerhit; // Whether enemy has hit the player

    public Transform SightPos; // Raycast point of origin
    public Transform attackPoint; // Point of origin for enemy hit box
    public Transform targetLocation; // Player loacation
    public Transform GroundR; // Point of origin for floor collider

    public PlayerMovement pRef; // Referance for player movement script
    public PlayerLife lifeRef; // Referance for player life script

    public int maxHealth = 100; // Enemy health
    int currentHealth; // Enemy current health

    public LayerMask playerMask; // Layer for collider and reaycast detection

    public Animator enemyAnim; // Enemy animation

    private Rigidbody2D rb; // Referance to enemy rigidbody 2D

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Setting cureent health equal to max health
        gameObject.tag = ("EnemyIdle"); // Setting enemy tag i.e. state
        rb = GetComponent<Rigidbody2D>(); // Setting rigidbody  2D
        EnemyFacingRight = true; // Setting enemies direction
        targetLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); // Setting enemy target
        canAttack = true; // Setting enemy ability to attack
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<EnemyStates>().EnemyState == "Active") // Enable enemy
        {
            EnemySight();
            EnemyPath();
            EnemyAttack();
        }

        if (!GetComponentInChildren<enemyAttackTimer>().attack) // Stopping enemy attack
        {
            attackTime -= Time.deltaTime;
            canAttack = false;

        }

        if(attackTime <= 0) // Resetting enemy attack
        {
            attackTime = 2;
            canAttack = true;
            GetComponentInChildren<enemyAttackTimer>().attack = true;

        }

    }

    public void TakeDamage(int damage) // Enemy taking damage
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();

        }
    }

    void EnemySight() //enemy line of sight
    {
        Debug.DrawRay(SightPos.position, SightPos.right); // Make raycast visible within sceneview

        RaycastHit2D hit = Physics2D.Raycast(SightPos.position, SightPos.right, maxDistance, playerMask); // Setting raycast

        if (hit.collider != null && pRef.visible) // If detects the player, becomes active
        {
            Debug.Log("PlayerHit");
            gameObject.tag = ("EnemyActive");

        }

        if (gameObject.tag == "EnemyActive") // Active time begins to countdown
        {
            activeStateTimer -= Time.deltaTime;

        }

        if(activeStateTimer <= 0 && hit.collider == null && gameObject.tag != ("EnemyAttack")) // Player is no longer within sights and timer reaches 0, returns to idle state
        {
            gameObject.tag = ("EnemyIdle");
            activeStateTimer = 1;

        }

        if(activeStateTimer <= 0 && hit.collider != null) // If player is still within sights when the timer eaches 0 then enemy will enter its attack state
        {
            gameObject.tag = ("EnemyAttack");

        }

        if (gameObject.tag == ("EnemyAttack")) // Enemy is within attack state 
        {

            if (hit.collider == null) // If enemy is within its attack state  but can no longer detect player, decrease timer
            {
                AttackStateTimer -= Time.deltaTime;
                Debug.Log("noPlayer");

            }

            if(AttackStateTimer <= 0) // If timer reaches 0 then reset the enemy state
            {
                gameObject.tag = ("EnemyIdle");
                activeStateTimer = 1;
                AttackStateTimer = 2;
            }

        }
    }

    void EnemyAttack()
    {
        Collider2D playerHitBox = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerMask); // Setting enemy hit box

        if(gameObject.tag == ("EnemyAttack") && isFloor) // If within an attack state and on the floor
        {
            GetComponent<SpriteRenderer>().color = Color.red; // Change the colour

            if(playerHitBox != null && canAttack) // If player is within hit box and enemy is able to attack, then enemy will attack
            {
                enemyAnim.SetTrigger("eAttack"); // Play enemy attack animation
                
            }

        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.blue; // If enemy is not in attack state then return to original colour
        }
        
    }

    void EnemyPath()
    {
        
        Collider2D hitGroundR = Physics2D.OverlapCircle(GroundR.position, WalkRange, 1 << 11); // Setting ground detection

        if (hitGroundR != null) // Ground is detected
        {
            Debug.Log("HitGround");
            isFloor = true;
        }
        
        if (gameObject.tag == "EnemyIdle") // Walking when in an idle state
        {
            canAttack = true;

            if (!EnemyFacingRight) //Moving Left
            {
                rb.velocity = new Vector3(-speed, 0f, 0f);

                if (hitGroundR == null) // Turn if no ground
                {
                    Flip();
                    Debug.Log("NoGroundL");
                }

            }
            else if (EnemyFacingRight) //Moving Right
            {
                rb.velocity = new Vector3(speed, 0f, 0f);

                if (hitGroundR == null) // Turn if no ground 
                {
                    Flip();
                    Debug.Log("NoGroundR");
                }


            }
        } 
        
        if(gameObject.tag == ("EnemyAttack")) // Running when in an attack state
        {
           
            if (EnemyFacingRight) //Turn if player is behind enemy
            {
                if (targetLocation.position.x < gameObject.transform.position.x)
                {
                    Flip();

                }
            }else if (!EnemyFacingRight) //Turn if player is behind enemy
            {
                if (targetLocation.position.x > gameObject.transform.position.x)
                {
                    Flip();
                }
                
            }

            if (Vector2.Distance(transform.position, targetLocation.position) > stopDistance) // Enemy running speed when in attack state
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetLocation.position.x, transform.position.y), AttackSpeed * Time.deltaTime);

            }
            

        }

        

    }

    void Die()
    {
        //die
        Debug.Log("Enemy Died");
        gameObject.SetActive(false);


    }

    void Flip() // Change direction
    {
        EnemyFacingRight = !EnemyFacingRight;


        transform.Rotate(0, 180, 0);
    }

    public void EnemeyStun() // Enemy Stunned
    {
        Debug.Log("enemyStun");

    }

    private void OnDrawGizmos()
    {
        if(GroundR.position == null) 
        {
            return;
        }

        //if (GroundL.position == null)
        //{
          //  return;
        //}

        Gizmos.DrawWireSphere(GroundR.position, WalkRange); // Draw floor detection box
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); // Draw hit box
    }
}
