using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public Animator lAnim;
    public float LifeNum;
    public float lTimer;
    public string rechargeState;


    // Start is called before the first frame update
    void Start()
    {
        LifeNum = 6f;
        lTimer = 2f;
        rechargeState = "Active";
    }

    // Update is called once per frame
    void Update()
    {
        LifeCounter();

        if (Input.GetButtonDown("Xbox_B"))
        {
            LifeNum -= 1;
            lTimer = 2f;


        }

        if(LifeNum < 6)
        {
            lTimer -= Time.deltaTime;

        }

        if(lTimer <= 0)
        {
            lTimer = 2f;
            LifeNum += 1;

        }

        if(LifeNum >= 6)
        {
            LifeNum = 6;
            lTimer = 2f;

        }
    }

    void LifeCounter()
    {
        if (LifeNum == 6f)
        {
            lAnim.SetBool("Full", true);
            lAnim.SetBool("5", false);
            lAnim.SetBool("4", false);
            lAnim.SetBool("3", false);
            lAnim.SetBool("2", false);
            lAnim.SetBool("1", false);
            lAnim.SetBool("0", false);
            lAnim.SetBool("Recharge", false);


        }
        else if (LifeNum == 5f)
        {
            lAnim.SetBool("Full", false);
            lAnim.SetBool("5", true);
            lAnim.SetBool("4", false);
            lAnim.SetBool("3", false);
            lAnim.SetBool("2", false);
            lAnim.SetBool("1", false);
            lAnim.SetBool("0", false);
            lAnim.SetBool("Recharge", false);

        }
        else if (LifeNum == 4f)
        {
            lAnim.SetBool("Full", false);
            lAnim.SetBool("5", false);
            lAnim.SetBool("4", true);
            lAnim.SetBool("3", false);
            lAnim.SetBool("2", false);
            lAnim.SetBool("1", false);
            lAnim.SetBool("0", false);
            lAnim.SetBool("Recharge", false);

        }
        else if (LifeNum == 3f)
        {
            lAnim.SetBool("Full", false);
            lAnim.SetBool("5", false);
            lAnim.SetBool("4", false);
            lAnim.SetBool("3", true);
            lAnim.SetBool("2", false);
            lAnim.SetBool("1", false);
            lAnim.SetBool("0", false);
            lAnim.SetBool("Recharge", false);

        }
        else if (LifeNum == 2f)
        {
            lAnim.SetBool("Full", false);
            lAnim.SetBool("5", false);
            lAnim.SetBool("4", false);
            lAnim.SetBool("3", false);
            lAnim.SetBool("2", true);
            lAnim.SetBool("1", false);
            lAnim.SetBool("0", false);
            lAnim.SetBool("Recharge", false);

        }
        else if (LifeNum == 1f)
        {
            lAnim.SetBool("Full", false);
            lAnim.SetBool("5", false);
            lAnim.SetBool("4", false);
            lAnim.SetBool("3", false);
            lAnim.SetBool("2", false);
            lAnim.SetBool("1", true);
            lAnim.SetBool("0", false);
            lAnim.SetBool("Recharge", false);

        }
        else if (LifeNum <= 0f)
        {
            lAnim.SetBool("Full", false);
            lAnim.SetBool("5", false);
            lAnim.SetBool("4", false);
            lAnim.SetBool("3", false);
            lAnim.SetBool("2", false);
            lAnim.SetBool("1", false);
            lAnim.SetBool("0", false);
            lAnim.SetBool("Recharge", true);
            rechargeState = "inActive";

        }

    }

    public void Recharge()
    {
        LifeNum = 6;
        rechargeState = "Active";

    }
}
