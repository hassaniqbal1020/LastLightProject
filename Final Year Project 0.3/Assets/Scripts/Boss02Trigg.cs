using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Trigg : MonoBehaviour
{
    public GameObject Boss02;

    private void Update()
    {
        Boss02 = GameObject.FindGameObjectWithTag("Boss02");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Boss02.GetComponent<FinalBoss>().playerEnter = true;
        }
    }
}
