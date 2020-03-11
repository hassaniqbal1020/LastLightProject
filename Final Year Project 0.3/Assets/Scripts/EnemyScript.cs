using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public float maxDistance;
    public LayerMask mask;
    public LayerMask playerMask;
    public PlayerMovement pRef;
    public PlayerLife lifeRef;

    public float attackTime;
    public bool canAttack;
    bool FacingRight;
    public Transform SightPos;
    public float activeTimer;
    bool Playerhit;
    public float speed;
    public float AttackSpeed;
    public float stopDistance;
    public float attackRange;
    public Transform attackPoint;
    public Animator enemyAnim;

    public float AttackTimer;
    bool isFloor;
    public Transform targetLocation;

    public Transform GroundR;
    //public Transform GroundL;
    public float WalkRange;
    //public LayerMask GroundLayers;

    [SerializeField]private bool EnemyFacingRight;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        gameObject.tag = ("EnemyIdle");
        rb = GetComponent<Rigidbody2D>();
        EnemyFacingRight = true;
        targetLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<EnemyStates>().EnemyState == "Active")
        {
            EnemySight();
            EnemyPath();
            EnemyAttack();
        }

        if (!GetComponentInChildren<enemyAttackTimer>().attack)
        {
            attackTime -= Time.deltaTime;
            canAttack = false;

        }

        if(attackTime <= 0)
        {
            attackTime = 2;
            canAttack = true;
            GetComponentInChildren<enemyAttackTimer>().attack = true;

        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();

        }
    }

    void EnemySight() //enemy line of sight
    {
        Debug.DrawRay(SightPos.position, SightPos.right);

        RaycastHit2D hit = Physics2D.Raycast(SightPos.position, SightPos.right, maxDistance, mask);

        if (hit.collider != null && pRef.visible)
        {
            Debug.Log("PlayerHit");
            gameObject.tag = ("EnemyActive");

        }

        if (gameObject.tag == "EnemyActive")
        {
            activeTimer -= Time.deltaTime;

        }

        if(activeTimer <= 0 && hit.collider == null && gameObject.tag != ("EnemyAttack"))
        {
            gameObject.tag = ("EnemyIdle");
            activeTimer = 1;

        }

        if(activeTimer <= 0 && hit.collider != null)
        {
            gameObject.tag = ("EnemyAttack");

        }

        if (gameObject.tag == ("EnemyAttack"))
        {

            if (hit.collider == null)
            {
                AttackTimer -= Time.deltaTime;
                Debug.Log("noPlayer");

            }

            if(AttackTimer <= 0)
            {
                gameObject.tag = ("EnemyIdle");
                activeTimer = 1;
                AttackTimer = 2;
            }

        }

        Debug.DrawRay(SightPos.position, SightPos.right);
    }

    void EnemyAttack()
    {
        Collider2D playerHitBox = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerMask);

        if(gameObject.tag == ("EnemyAttack") && isFloor)
        {
            GetComponent<SpriteRenderer>().color = Color.red;

            if(playerHitBox != null && canAttack)
            {
                enemyAnim.SetTrigger("eAttack");
                
            }

        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        
    }

    void EnemyPath()
    {
        
        Collider2D hitGroundR = Physics2D.OverlapCircle(GroundR.position, WalkRange, 1 << 11);
        //Collider2D hitGroundL = Physics2D.OverlapCircle(GroundL.position, WalkRange, GroundLayers);

        if (hitGroundR != null)
        {
            Debug.Log("HitGround");
            isFloor = true;
        }
        
        if (gameObject.tag == "EnemyIdle")
        {
            canAttack = true;

            if (!EnemyFacingRight) //Moving Left
            {
                rb.velocity = new Vector3(-speed, 0f, 0f);
                if (hitGroundR == null)
                {
                    Flip();
                    Debug.Log("NoGroundL");
                }

            }
            else if (EnemyFacingRight) //Moving Right
            {
                rb.velocity = new Vector3(speed, 0f, 0f);
                if (hitGroundR == null)
                {
                    Flip();
                    Debug.Log("NoGroundR");
                }


            }
        } 
        
        if(gameObject.tag == ("EnemyAttack"))
        {
           
            if (EnemyFacingRight)
            {
                if (targetLocation.position.x < gameObject.transform.position.x)
                {
                    Flip();

                }
            }else if (!EnemyFacingRight)
            {
                if (targetLocation.position.x > gameObject.transform.position.x)
                {
                    Flip();
                }
                
            }

            if (Vector2.Distance(transform.position, targetLocation.position) > stopDistance)
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

    void Flip()
    {
        EnemyFacingRight = !EnemyFacingRight;


        transform.Rotate(0, 180, 0);
    }

    public void EnemeyStun()
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

        Gizmos.DrawWireSphere(GroundR.position, WalkRange);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
