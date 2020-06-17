using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float maxDistance; // Max distance of the enemy raycast
    public float attackTime; // Time until next attack
    public float waitTime;
    public float activeStateTimer; // Timer for when enemy becomes active before attacking
    public float speed; // Walking speed of the player
    public float AttackSpeed; // Speed of enemy when in attacking state
    public float stopDistance; // Stopping distance between enemy and player
    public float attackRange; // Range of enemy attack
    public float AttackStateTimer; // Timer for enemy attack state
    public float WalkRange; // Radius of floor collider
    public float WallRange; // Radius of wall collider
    public float hurtTimer; // Timer for when enemy is hurt
    public float floorTimer;
    public float deathTimer;


    [SerializeField] private bool EnemyFacingRight; // Direction the enemy is facing
    public bool canAttack; // Whether enemy can attack or not
    public bool canRun; // if enemy can run
    public bool enemyNear; // if player is near
    public bool hitByPlayer; // if hit by player
    bool FacingRight; // Direction the enemy is facing
    public bool isFloor; // Whether floor is in front of the enemy
    bool Playerhit; // Whether enemy has hit the player
    bool isWall; // Whether wall is in fornt of enemy

    public Transform SightPos; // Raycast point of origin
    public Transform attackPoint; // Point of origin for enemy hit box
    public Transform targetLocation; // Player loacation
    public Transform GroundR; // Point of origin for floor collider
    public Transform WallCheck; // Point of origin for wall collider

    public PlayerMovement pRef; // Referance for player movement script

    public int maxHealth = 125; // Enemy health
    public int currentHealth; // Enemy current health

    public LayerMask playerMask; // Layer for collider and raycast detection
    public LayerMask WallLayerMask; // Layer for wall collision
    public LayerMask HideMask; //Layer for detection
    public LayerMask GroundMask;

    public Rigidbody2D rb; // Referance to enemy rigidbody 2D

    public Animator enemyAnim;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Setting cureent health equal to max health
        gameObject.tag = ("EnemyIdle"); // Setting enemy tag i.e. state
        rb = GetComponent<Rigidbody2D>(); // Setting rigidbody  2D
        EnemyFacingRight = true; // Setting enemies direction
        canAttack = true; // Setting enemy ability to attack
        canRun = true;
        hurtTimer = 1f;
        floorTimer = 0.2f;
        deathTimer = 0.7f;
        

    }

    // Update is called once per frame
    void Update()
    {
        targetLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); // Setting enemy target
        pRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); // Setting ref to player script

        if (GetComponent<EnemyStates>().EnemyState == "Active") // Enable enemy
        {
            EnemySight();
            EnemyPath();
            EnemyAttack();
        }

        if (!GetComponentInChildren<enemyAttackFunction>().attack) // Stopping enemy attack
        {
            attackTime -= Time.deltaTime;
            

        }

        if(attackTime <= 0) // Resetting enemy attack
        {
            attackTime = 1.5f;
            canAttack = true;
            GetComponentInChildren<enemyAttackFunction>().attack = true;

        }

        if (hitByPlayer)
        {
            hurtTimer -= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = Color.white;

            if (!canAttack)
            {
                canAttack = true;

            }
        }

        if(hurtTimer <= 0)
        {
            hurtTimer = 1f;
            hitByPlayer = false;

            if (gameObject.tag != "EnemyAttack")
            {
                gameObject.tag = "EnemyAttack";
            }
        }

        if (currentHealth <= 0)
        {

            Die();

        }

    }

    public void TakeDamage(int damage) // Enemy taking damage
    {
        currentHealth -= damage;


    }

    void EnemySight() //enemy line of sight
    {
        Debug.DrawRay(SightPos.position, SightPos.right); // Make raycast visible within sceneview

        RaycastHit2D hit = Physics2D.Raycast(SightPos.position, SightPos.right, maxDistance, playerMask); // Setting raycast for player detection
        RaycastHit2D hide = Physics2D.Raycast(SightPos.position, SightPos.right, maxDistance, HideMask); // Setting raycast for detection

        if (hit.collider != null && hide.collider == null && pRef.visible && gameObject.tag != "EnemyStun") // If detects the player, becomes active
        {
            gameObject.tag = ("EnemyActive");
            
        }

        if (gameObject.tag == "EnemyActive") // Active time begins to countdown
        {
            activeStateTimer -= Time.deltaTime;

        }

        if(activeStateTimer <= 0 && hit.collider == null && gameObject.tag != ("EnemyAttack") && gameObject.tag != "EnemyStun") // Player is no longer within sights and timer reaches 0, returns to idle state
        {
            gameObject.tag = ("EnemyIdle");
            activeStateTimer = 0.15f;

        }

        if(activeStateTimer <= 0 && hit.collider != null && gameObject.tag != "EnemyStun") // If player is still within sights when the timer eaches 0 then enemy will enter its attack state
        {
            gameObject.tag = ("EnemyAttack");
            AttackStateTimer = 2;
        }

        if (gameObject.tag == ("EnemyAttack") && gameObject.tag != "EnemyStun") // Enemy is within attack state 
        {

            if (hit.collider == null) // If enemy is within its attack state  but can no longer detect player, decrease timer
            {
                AttackStateTimer -= Time.deltaTime;


            }

            if(AttackStateTimer <= 0) // If timer reaches 0 then reset the enemy state
            {
                gameObject.tag = ("EnemyIdle");
                activeStateTimer = 0.15f;
                AttackStateTimer = 2;
            }

        }
    }

    void EnemyAttack()
    {
        Collider2D playerHitBox = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerMask); // Setting enemy hit box

        if(currentHealth > 0)
        {
            if (gameObject.tag == ("EnemyAttack") && isFloor && gameObject.tag != "EnemyStun") // If within an attack state and on the floor
            {
                //GetComponent<SpriteRenderer>().color = Color.red; // Change the colour
                GetComponent<Dissolver>().isAttack = true;

                if (playerHitBox != null) // If player is within hit box and enemy is able to attack, then enemy will attack
                {
                    enemyNear = true;

                    if (canAttack)
                    {
                        enemyAnim.SetTrigger("isAttack"); // Play enemy attack animation
                        canAttack = false;
                    }

                }

                if (playerHitBox == null)
                {
                    enemyNear = false;

                }



            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.blue; // If enemy is not in attack state then return to original colour
                GetComponent<Dissolver>().isAttack = false;

            }
        }
        

    }

    void EnemyPath()
    {
        
        Collider2D hitGroundR = Physics2D.OverlapCircle(GroundR.position, WalkRange, GroundMask); // Setting ground detection

        Collider2D WallHit = Physics2D.OverlapCircle(WallCheck.position, WallRange, WallLayerMask); // Setting wall detection

        if(WallHit != null)
        {
            isWall = true;

        }

        if (hitGroundR != null) // Ground is detected
        {

            isFloor = true;
            floorTimer = 0.2f;
            speed = 1.7f;

        }else if(hitGroundR == null)
        {
            isFloor = false;
            floorTimer -= Time.deltaTime;
        }

        if (floorTimer <= 0 && !isFloor)
        {
            speed = 0;
        }

        if (gameObject.tag == "EnemyIdle" && gameObject.tag != "EnemyStun") // Walking when in an idle state
        {
            canAttack = true;

            if (!hitByPlayer)
            {
                if (!EnemyFacingRight) //Moving Left
                {
                    rb.velocity = new Vector3(-speed, 0f, 0f);

                    if (hitGroundR == null && floorTimer > 0 || hitGroundR != null && WallHit != null && floorTimer > 0) // Turn if no ground
                    {
                        Flip(true); 

                    }

                }
                else if (EnemyFacingRight) //Moving Right
                {
                    rb.velocity = new Vector3(speed, 0f, 0f);

                    if (hitGroundR == null && floorTimer > 0 || hitGroundR != null && WallHit != null && floorTimer > 0) // Turn if no ground 
                    {
                        Flip(true);

                    }


                }
            }

        } 
        
        if(gameObject.tag == ("EnemyAttack") && !hitByPlayer && gameObject.tag != "EnemyStun") // Running when in an attack state
        {
           
            if (EnemyFacingRight) //Turn if player is behind enemy
            {
                if (targetLocation.position.x < gameObject.transform.position.x)
                {
                    Flip(true);

                }else if(hitGroundR == null)
                {
                    Flip(true);
                }
            }else if (!EnemyFacingRight) //Turn if player is behind enemy
            {
                if (targetLocation.position.x > gameObject.transform.position.x)
                {
                    Flip(true);
                }
                else if (hitGroundR == null)
                {
                    Flip(true);
                }

            }

            if (!canRun)
            {
                waitTime -= Time.deltaTime;

            }

            if(waitTime <= 0)
            {
                canRun = true;
                waitTime = 1.2f;
            }

            if(hitGroundR != null && canRun)
            {
                if (Vector2.Distance(transform.position, targetLocation.position) > stopDistance) // Enemy running speed when in attack state
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetLocation.position.x, transform.position.y), AttackSpeed * Time.deltaTime);

                }

            }else if(hitGroundR == null)
            {
                gameObject.tag = ("EnemyIdle");
                activeStateTimer = 0.15f;
                AttackStateTimer = 2;
            }


        }

        

    }

    void Die()
    {
        //die
        GetComponent<Dissolver>().isDisolving = true;
        deathTimer -= Time.deltaTime;
        enemyAnim.speed = 0;
        

        if(deathTimer <= 0)
        {

            if (pRef.LifeRadial.fillAmount < 1)
            {
                pRef.LifeRadial.fillAmount += 0.1f;
            }

            gameObject.SetActive(false);
        }
    }

    public void Flip(bool enableFlip) // Change direction
    {
        if (enableFlip)
        {
            EnemyFacingRight = !EnemyFacingRight;

        }

        transform.Rotate(0, 180, 0);
        //HealthBar.transform.Rotate(0, 180, 0);
    }

    public void EnemeyStun() // Enemy Stunned
    {

        gameObject.tag = "EnemyStun";

        enemyAnim.SetTrigger("isStun");

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
        Gizmos.DrawWireSphere(WallCheck.position, WallRange); // Draw hit box
    }
}
