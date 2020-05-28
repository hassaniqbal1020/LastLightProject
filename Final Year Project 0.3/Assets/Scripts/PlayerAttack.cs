using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerLife UIref;
    public float attackRate;
    float nextAtkTime;
    public PlayerLife lref;



    //public GameObject pRef;

    void Start()
    {
        attackRate = 3f;
        nextAtkTime = 0;


    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAtkTime)
        {
            if (UIref.rechargeState == "Active")
            {
                if (Input.GetButtonDown("Xbox_B"))
                {
                    Attack();

                    nextAtkTime = Time.time + 1f / attackRate;

                    lref.atkAnimEnable = true;
                }

            }

        }
        
        
    }

    void Attack()
    {
        GetComponentInParent<PlayerMovement>().playerAnim.SetTrigger("isAttack");

    }


}
