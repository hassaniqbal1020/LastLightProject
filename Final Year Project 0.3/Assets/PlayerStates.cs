using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public Main mRef;
    public string PlayerState;
    public bool isDead;
    public GameObject dPart;

    // Start is called before the first frame update
    void Start()
    {
        PlayerState = "inActive";

    }

    // Update is called once per frame
    void Update()
    {

        if(mRef.MainState == "Play" && !isDead)
        {
            PlayerState = "Active";
        }
        else
        {
            PlayerState = "inActive";
        }

        if(mRef.MainState == "Play" && isDead)
        {
            PlayerState = "Dead";
            GetComponent<checkpoint>().isDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            isDead = true;
            

        }

    }

}
