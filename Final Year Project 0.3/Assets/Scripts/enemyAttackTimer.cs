using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackTimer : MonoBehaviour
{
    public bool attack;
    public PlayerMovement pRef;
    public int eDamage;

    private void Awake()
    {
        attack = true;
        pRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); // Setting ref to player script
    }

    void AttackTimmer()
    {
        if(GetComponentInParent<EnemyScript>().enemyNear == true)
        {
            pRef.LifeRadial.fillAmount -= 0.1f;
            attack = false;
            Debug.Log("eHit");
            GetComponentInParent<EnemyScript>().canRun = false;
            pRef.hitByEnemy = true;

        }
        
    }
}
