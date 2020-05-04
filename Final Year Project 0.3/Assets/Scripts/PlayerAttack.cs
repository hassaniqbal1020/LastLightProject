using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerLife UIref;

    //public GameObject pRef;

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if(UIref.rechargeState == "Active")
        {
            if (Input.GetButtonDown("Xbox_B"))
            {
                Attack();

                Debug.Log("B_Pressed");
            }

        }
        
    }

    void Attack()
    {
        GetComponentInParent<PlayerMovement>().playerAnim.SetTrigger("isAttack");

    }


}
