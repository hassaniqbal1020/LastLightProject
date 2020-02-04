using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bRef;

    public string ShootState;

    public float shootMetre;
    public float shootTimer;


    // Start is called before the first frame update
    void Start()
    {
        ShootState = "nShoot";
        shootMetre = 3f;
        shootTimer = 2f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Xbox_B") && shootMetre > 0)
        {
            Instantiate(bRef, transform.position, transform.rotation);
            shootMetre -= 1f;


        }

        if(shootMetre <= 0)
        {
            shootTimer -= Time.deltaTime;

        }

        if(shootTimer <= 0)
        {
            shootMetre = 3f;
            shootTimer = 2f;


        }
    }
}
