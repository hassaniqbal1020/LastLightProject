using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBoss : MonoBehaviour
{
    public Animator Boss02Anim;

    public bool playerEnter;
    public bool isIdle;

    public float AtkTimer;
    public float BossHealth;

    public GameObject Boss02Trigger;
    public GameObject Bomb01;
    public GameObject Bomb02;
    public GameObject Spikes;
    public GameObject SpawnPoint;
    public GameObject Boss02HpBar;

    // Start is called before the first frame update
    void Start()
    {
        AtkTimer = 1.5f;
        BossHealth = 1000f;
        isIdle = false;
    }

    // Update is called once per frame
    void Update()
    {
        Boss02Trigger = GameObject.FindGameObjectWithTag("Boss2Trigg");
        Boss02HpBar = GameObject.FindGameObjectWithTag("BossUI").transform.GetChild(1).gameObject;
        Boss02HpBar.GetComponent<Slider>().value = BossHealth;

        if (BossHealth <= 0)
        {
            Boss02Anim.speed = 0;
            Boss02HpBar.SetActive(false);
            Destroy(gameObject, 1);
        }

        if (Boss02Trigger == null)
        {
            return;
        }

        if (playerEnter)
        {
            Boss02Anim.SetTrigger("PlayerEnter");
            AtkTimer -= Time.deltaTime;
            Boss02HpBar.SetActive(true);

        }

        if (!playerEnter)
        {
            Boss02HpBar.SetActive(false);

        }

        if (AtkTimer <= 0)
        {
            Boss02Trigger.SetActive(false);
            AtkTimer = 1.5f;
            playerEnter = false;
            Boss02Anim.SetInteger("AttackString", 1);
        }
    }

    public void TakeDamage(int damage)
    {
        BossHealth -= damage;

    }

    void ResetAttack()
    {
        if(BossHealth > 650)
        {
            Boss02Anim.SetInteger("AttackString", Random.Range(1, 3));

        }
        
        if(BossHealth > 450 && BossHealth <= 650)
        {
            Boss02Anim.SetInteger("AttackString", Random.Range(1, 4));

        }

        if(BossHealth <= 450)
        {
            Boss02Anim.SetInteger("AttackString", Random.Range(1, 5));
        }
    }

    void UseExplosives()
    {
        Instantiate(Bomb01, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

    }

    void UseBigBombAttack()
    {
        if(BossHealth < 500)
        {
            Instantiate(Bomb02, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

        }
    }
    void UseSpikes()
    {
        Instantiate(Spikes, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
    }

    void BossIdle()
    {
        isIdle = true;
    }

    void BossNotIdle()
    {
        isIdle = false;
    }

    void BossNotHit()
    {
        Boss02Anim.SetBool("isHit", false);

    }
}
