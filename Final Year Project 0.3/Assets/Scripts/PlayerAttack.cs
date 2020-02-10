using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator swordAnimator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 40;
    //public GameObject pRef;


    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<PlayerStates>().PlayerState == "Active")
        {
            if (Input.GetButtonDown("Xbox_B") && gameObject.GetComponentInParent<LifeForce>().currentLife > 0 && gameObject.GetComponentInParent<LifeForce>().LifeState == "Active")
            {
                Attack();

                Debug.Log("B_Pressed");
            }

        }
        
    }

    void Attack()
    {
        //playnanim
        swordAnimator.SetTrigger("Attack");


        //enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit" + enemy.name);
            enemy.GetComponent<EnemyScript>().TakeDamage(attackDamage);



        }


    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;

        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }
}
