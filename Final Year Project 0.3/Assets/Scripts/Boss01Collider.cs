using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01Collider : MonoBehaviour
{
    public Transform AttackPoint;
    public float AttackRange;
    public LayerMask PlayerHitLayer;

    public PlayerMovement pRef;

    public GameObject PlayerRef;

    public float hitForce;
    public float AttackTimer;

    public GameObject hitEffect;


    // Start is called before the first frame update
    void Start()
    {
        pRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        AttackTimer = 1.5f;
        hitForce = 5f;

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D hitBox = Physics2D.OverlapCircle(AttackPoint.position, AttackRange, PlayerHitLayer);

        if(hitBox != null)
        {
            AttackRange = 0;

            Debug.Log("bossHitPlayer");
            pRef.LifeRadial.fillAmount -= 0.1f;
            pRef.timeFreeze = true;
            pRef.hitByEnemy = true;
            Instantiate(hitEffect, PlayerRef.transform.position, PlayerRef.transform.rotation);
        }

        if(AttackRange == 0)
        {
            AttackTimer -= Time.deltaTime;
        }

        if(AttackTimer <= 0)
        {
            AttackTimer = 1.5f;
            AttackRange = 1;

        }
    }

    private void OnDrawGizmos()
    {
        if(AttackPoint.position == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);


    }
}
