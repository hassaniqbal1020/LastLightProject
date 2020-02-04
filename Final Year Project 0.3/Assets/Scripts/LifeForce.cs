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


    // Start is called before the first frame update
    void Start()
    {
        maxLife = 1000f;
        Lforce.maxValue = maxLife;
        Lforce.value = maxLife;
        currentLife = maxLife;
        waitTime = 1.5f;

    }

    // Update is called once per frame
    void Update()
    {

        currentLife = Lforce.value;

        if (Input.GetButtonDown("Xbox_B"))
        {
            Lforce.value -= maxLife * 0.1f;
            waitTime = 3f;
            LAnim.SetTrigger("Used");

        }

        if(Lforce.value < maxLife)
        {
            waitTime -= 0.05f;
        }

        if(waitTime < 0)
        {
            StartCoroutine(Regen());
        }

        if(Lforce.value == maxLife)
        {
            waitTime = 3f;
        }

    }

    IEnumerator Regen()
    {
        yield return new WaitForSeconds(0.5f);
        Lforce.value += maxLife * 0.005f;
        Debug.Log("Regen");

        

    }


}