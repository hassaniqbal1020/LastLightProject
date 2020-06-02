using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public Animator Boss02Anim;

    public bool playerEnter;

    public float AtkTimer;
    public float BossHealth;

    public GameObject Boss02Trigger;
    public GameObject Bomb01;
    public GameObject Bomb02;
    public GameObject SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        AtkTimer = 1.5f;
        BossHealth = 800f;
    }

    // Update is called once per frame
    void Update()
    {
        Boss02Trigger = GameObject.FindGameObjectWithTag("Boss2Trigg");

        if(Boss02Trigger == null)
        {
            return;
        }

        if (playerEnter)
        {
            Boss02Anim.SetTrigger("PlayerEnter");
            AtkTimer -= Time.deltaTime;
            
        }

        if(AtkTimer <= 0)
        {
            Boss02Trigger.SetActive(false);
            AtkTimer = 1.5f;
            playerEnter = false;
            Boss02Anim.SetInteger("AttackString", 1);
        }
    }

    void ResetAttack()
    {
        if(BossHealth > 300)
        {
            Boss02Anim.SetInteger("AttackString", Random.Range(1, 3));

        }
        
        if(BossHealth <= 300)
        {
            Boss02Anim.SetInteger("AttackString", Random.Range(1, 4));

        }
    }

    void UseExplosives()
    {
        Instantiate(Bomb01, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

    }

    void UseHomingAttack()
    {
        if (BossHealth <= 300)
        {
            Instantiate(Bomb02, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
        }
    }
}
