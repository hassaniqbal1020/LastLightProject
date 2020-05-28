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
    public LayerMask bossLayer;

    public GameObject hitEffect;

    void Start()
    {
        hitForce = 2;
    }

    void Update()
    {
        attackDamage = GetComponentInParent<PlayerMovement>().AttackPower;
    }

    void Attack()
    {
        //enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyScript>().TakeDamage(attackDamage);
            enemy.GetComponent<Rigidbody2D>().AddForce(transform.right * hitForce, ForceMode2D.Impulse);
            enemy.GetComponent<EnemyScript>().hitByPlayer = true;
            enemy.GetComponent<EnemyScript>().enemyAnim.SetTrigger("isHurt");
            Instantiate(hitEffect, enemy.transform.position, enemy.transform.rotation);
            

        }

        Collider2D BossHit = Physics2D.OverlapCircle(attackPoint.position, attackRange, bossLayer);

        if(BossHit != null)
        {
            Instantiate(hitEffect, BossHit.transform.position, BossHit.transform.rotation);
            BossHit.GetComponent<Boss01>().TakeDamage(attackDamage);

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
