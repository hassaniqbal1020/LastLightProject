using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeForce : MonoBehaviour
{
    public Slider Lforce;
    public float maxLife;
    public float currentLife;

    public bool Recharge;
    public float waitTime;
    public float regenValue;

    public bool Active;

    public Animator LAnim;

    public string LifeState;

    // Start is called before the first frame update
    void Start()
    {
        maxLife = 1000f;
        Lforce.maxValue = maxLife;
        Lforce.value = maxLife;
        currentLife = maxLife;
        waitTime = 3f;
        LifeState = "Active";
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<PlayerStates>().PlayerState == "Active")
        {
            if (Input.GetButtonDown("Xbox_B") && LifeState == "Active")
            {
                waitTime = 3f;
                Active = true;

                if (gameObject.GetComponent<PlayerMovement>().InventoryNumber == 1)
                {
                    if (gameObject.GetComponentInChildren<Shoot>().shootMetre > 0)
                    {
                        Lforce.value -= maxLife * 0.24f;
                        LAnim.SetTrigger("Used");
                    }

                }
                else if (gameObject.GetComponent<PlayerMovement>().InventoryNumber == 2)
                {
                    Lforce.value -= maxLife * 0.08f;
                    LAnim.SetTrigger("Used");
                }



            }
            else
            {
                Active = false;

            }

            
        }
        currentLife = Lforce.value;

        

        if(Lforce.value < maxLife)
        {
            waitTime -= 0.05f;
        }

        if(waitTime < 0)
        {
            StartCoroutine(Regen());
            
        }

        if(Lforce.value <= 0)
        {
            LifeState = "inActive";

        }

        if(Lforce.value >= maxLife)
        {
            waitTime = 3f;

            if(LifeState == "inActive")
            {
                LifeState = "Active";
            }
            

        }
    }

    IEnumerator Regen()
    {
        yield return new WaitForSeconds(1f);
        if (!Active && waitTime < 0)
        {
            Lforce.value += maxLife * regenValue * Time.deltaTime;
            Debug.Log(regenValue);
        }
        yield return new WaitForSeconds(0.5f);
        if (Active)
        {
            yield return null;
        }
    }


}