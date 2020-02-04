using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public float maxDistance;
    public LayerMask mask;
    bool FacingRight;
    public Transform SightPos;
    float activeTimer = 1f;
    bool Playerhit;

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


    }

    // Update is called once per frame
    void Update()
    {

        EnemySight();
        EnemyPath();

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();

        }
    }

    void EnemySight()
    {
        RaycastHit2D hit = Physics2D.Raycast(SightPos.position, SightPos.right, maxDistance, mask);

        if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            Debug.Log("PlayerHit");
            gameObject.tag = ("EnemyActive");


        }

        if (gameObject.tag == "EnemyActive")
        {
            activeTimer -= Time.deltaTime;

        }

        Debug.DrawRay(SightPos.position, SightPos.right);
    }

    void EnemyPath()
    {
        Collider2D hitGroundR = Physics2D.OverlapCircle(GroundR.position, WalkRange, 1 << 11);
        //Collider2D hitGroundL = Physics2D.OverlapCircle(GroundL.position, WalkRange, GroundLayers);
        if(gameObject.tag == "EnemyIdle")
        {
            if (!EnemyFacingRight) //Moving Left
            {
                rb.velocity = new Vector3(-1f, 0f, 0f);
                if (hitGroundR == null)
                {
                    Flip();
                    Debug.Log("NoGroundL");
                }

            }
            else if (EnemyFacingRight) //Moving Right
            {
                rb.velocity = new Vector3(1f, 0f, 0f);
                if (hitGroundR == null)
                {
                    Flip();
                    Debug.Log("NoGroundR");
                }


            }

            if (hitGroundR != null)
            {
                Debug.Log("HitGround");
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
        //Gizmos.DrawWireSphere(GroundL.position, WalkRange);
    }
}
