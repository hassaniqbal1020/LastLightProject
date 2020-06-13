using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss01 : MonoBehaviour
{
    //public Animator bossAnim;

    public float BossHP;
    public float animTimer;

    public bool isPlayerDead;
    public bool animPaused;

    public GameObject pRef;
    public GameObject BossTrigger;
    public GameObject WallBlock;
    public GameObject BossHPBar;
    public GameObject BossAttack;

    // Start is called before the first frame update
    void Start()
    {
        BossHP = 400f;
        pRef = GameObject.FindGameObjectWithTag("Player");
        animTimer = 1.5f;
        animPaused = false;

    }

    // Update is called once per frame
    void Update()
    {
        BossTrigger = GameObject.FindGameObjectWithTag("BossTrig");
        BossHPBar = GameObject.FindGameObjectWithTag("BossUI").transform.GetChild(0).gameObject;
        WallBlock = GameObject.FindGameObjectWithTag("BossControl").transform.GetChild(0).gameObject;
        BossAttack = GameObject.FindGameObjectWithTag("BossAttck");
        BossHPBar.GetComponent<Slider>().value = BossHP;
        BossHPBar.SetActive(true);

        if (animPaused)
        {
            animTimer -= Time.deltaTime;
            BossAttack.GetComponent<Boss01Collider>().AttackRange = 0;
            GetComponentInParent<Animator>().speed = 0;
        }

        if(animTimer <= 0)
        {
            animTimer = 1.5f;
            BossAttack.GetComponent<Boss01Collider>().AttackRange = 0.7f;
            GetComponentInParent<Animator>().speed = 1;
            animPaused = false;

        }

        if (BossHP <= 0 && BossHPBar.GetComponent<Slider>().value <= 0)
        {
            if (pRef.GetComponent<PlayerMovement>().LifeRadial.fillAmount < 1)
            {
                pRef.GetComponent<PlayerMovement>().LifeRadial.fillAmount = 1;
            }

            Die();
        }
    }

    public void TakeDamage (int damage)
    {
        BossHP -= damage;

    }

    void Die()
    {
        this.transform.parent.gameObject.SetActive(false);
        BossHPBar.SetActive(false);
        BossTrigger.SetActive(false);
        WallBlock.SetActive(false);
    }

}
