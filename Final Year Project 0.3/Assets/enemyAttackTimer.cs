using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackTimer : MonoBehaviour
{
    public bool attack;
    public PlayerLife lifeRef;
    public int eDamage;

    private void Awake()
    {
        attack = true;

    }

    void AttackTimmer()
    {
        attack = false;
        lifeRef.LifeNum -= eDamage;
        Debug.Log("eHit");

    }
}
