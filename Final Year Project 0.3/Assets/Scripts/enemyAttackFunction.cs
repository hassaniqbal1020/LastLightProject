using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class enemyAttackFunction : MonoBehaviour
{
    public PlayerMovement pRef;

    public GameObject playerRb;

    public float EnemyhitForce;

    public bool attack;

    public GameObject hitEffect;

    private void Awake()
    {
        pRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); // Setting ref to player script
        playerRb = GameObject.FindGameObjectWithTag("Player");

        EnemyhitForce = 5;

        attack = true;
    }

    void enemyAttack()
    {
        if (GetComponentInParent<EnemyScript>().enemyNear == true)
        {
            attack = false;
            GetComponentInParent<EnemyScript>().canRun = false;
            pRef.LifeRadial.fillAmount -= 0.15f;
            pRef.hitByEnemy = true;
            //pRef.timeFreeze = true;
            playerRb.GetComponent<Rigidbody2D>().AddForce(transform.right * EnemyhitForce, ForceMode2D.Impulse);
            Instantiate(hitEffect, playerRb.transform.position, playerRb.transform.rotation);
        }
        
    }

    void StunRecover()
    {
        transform.parent.tag = "EnemyIdle";
    }
}
