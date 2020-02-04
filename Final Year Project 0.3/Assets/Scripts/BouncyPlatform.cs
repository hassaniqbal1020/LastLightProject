using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{
    BoxCollider2D coll;
    public PhysicsMaterial2D Bouncy;
    public PhysicsMaterial2D noBouncy;


    public float bTimer = 3f;
    string bState = "Bounce";
    float bNum = 1f;



    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if(bNum == 0)
        {
            bTimer -= Time.deltaTime;
            bState = "NoBounce";

        }

        if(bTimer <= 0)
        {
            bTimer = 3f;
            bNum = 1f;

        }

        if(bNum == 1)
        {
            bState = "Bounce";

        }

        if(bState == "Bounce")
        {
            coll.sharedMaterial = Bouncy;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        
        if(bState == "NoBounce")
        {
            coll.sharedMaterial = noBouncy;
            gameObject.GetComponent<Renderer>().material.color = Color.magenta;

        }
        


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && bState == "Bounce")
        {

            bNum = 0f;

        }


    }
}
