using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bRef;
    public PlayerLife UIref;
    public float shootMetre;
    public float shootTimer;

    // Start is called before the first frame update
    void Start()
    {
        shootMetre = 4f;
        shootTimer = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if(UIref.rechargeState == "Active")
        {
            if (Input.GetButtonDown("Xbox_B") && shootMetre > 0)
            {
                Instantiate(bRef, transform.position, transform.rotation);
                shootMetre -= 1f;


            }

            if (shootMetre <= 0)
            {
                shootTimer -= Time.deltaTime;

            }

            if (shootTimer <= 0)
            {
                shootMetre = 4f;
                shootTimer = 0.01f;


            }

        }
        
    }
}
