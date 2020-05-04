using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFunction : MonoBehaviour
{
    public int attackDamage = 25;
    public float hitForce;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    void Start()
    {
        attackDamage = 25;
        hitForce = 2;
    }

    void Attack()
    {
        //enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit" + enemy.name);
            enemy.GetComponent<EnemyScript>().TakeDamage(attackDamage);
            
            enemy.transform.position += new Vector3(0, 0.1f, 0);

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;

        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }
}
