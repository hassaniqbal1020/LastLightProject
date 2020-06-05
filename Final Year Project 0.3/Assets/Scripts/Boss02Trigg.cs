using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Trigg : MonoBehaviour
{
    public GameObject Boss02;
    public GameObject WallBlock;

    public bool wallOn;

    private void Start()
    {
        wallOn = false;

    }

    void Update()
    {
        Boss02 = GameObject.FindGameObjectWithTag("Boss02");
        WallBlock = GameObject.FindGameObjectWithTag("FinalBossWall");

        if (!wallOn)
        {
            WallBlock.GetComponent<SpriteRenderer>().enabled = false;
            WallBlock.GetComponent<PolygonCollider2D>().enabled = false;

        }else if (wallOn)
        {
            WallBlock.GetComponent<SpriteRenderer>().enabled = true;
            WallBlock.GetComponent<PolygonCollider2D>().enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Boss02.GetComponent<FinalBoss>().playerEnter = true;
            wallOn = true;

        }
    }
}
