using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    public Main mRef; // Reference to main script
    public string EnemyState; // Enemy State

    // Start is called before the first frame update
    void Start()
    {
        mRef = GameObject.FindGameObjectWithTag("Main").GetComponent<Main>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<EnemyScript>().currentHealth > 0) // Enabling enemy
        {
            EnemyState = "Active";
        }
        else 
        {
            EnemyState = "inActive";
        }
    }
}
