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

        currentLife = Lforce.value;

        if (Input.GetButtonDown("Xbox_B") && LifeState == "Active")
        {
            waitTime = 3f;

            if (gameObject.GetComponent<PlayerMovement>().InventoryNumber == 2)
            {
                if (gameObject.GetComponentInChildren<Shoot>().shootMetre > 0)
                {
                    Lforce.value -= maxLife * 0.25f;
                    LAnim.SetTrigger("Used");
                }

            }else if(gameObject.GetComponent<PlayerMovement>().InventoryNumber == 1)
            {
                Lforce.value -= maxLife * 0.08f;
                LAnim.SetTrigger("Used");
            }
            
            

        }

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
        yield return new WaitForSeconds(0.5f);
        Lforce.value += maxLife * 0.005f;
        Debug.Log("Regen");

        

    }


}